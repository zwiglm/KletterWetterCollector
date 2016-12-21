using BaseService.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.DataAccess.Implementation
{
    public class BaseDao : IBaseDao
    {
        private IDatabaseConfiguration _databaseConfig;


        public BaseDao(IApplicationConfiguration appConfig, IDatabaseConfiguration databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }


        public int insertBaseDataCmd(ApiCommands.UpdBaseDataCmd baseCommand)
        {
            string sqlInsert = @"
                                INSERT into arduBase 
                                (Created, Temp, PwrWarn)
                                OUTPUT INSERTED.Id
                                VALUES (@created, @temperatur, @pwrWarn)
                                ";

            int insertedRows = 0;
            using (var dbConn = this.getMainDbConn())
            {
                SqlTransaction trans = null;
                try
                {
                    dbConn.Open();
                    trans = dbConn.BeginTransaction();

                    using (var command = new SqlCommand(sqlInsert, dbConn, trans))
                    {
                        command.Parameters.AddWithValue("@created", baseCommand._created);
                        command.Parameters.AddWithValue("@temperatur", baseCommand._temperature);
                        command.Parameters.AddWithValue("@pwrWarn", baseCommand._pwrWarn);
                        insertedRows = command.ExecuteNonQuery();
                    }

                    trans.Commit();
                }
                catch (Exception)
                {
                    if (trans != null) trans.Rollback();
                }

                if (dbConn != null)
                    dbConn.Close();
                return insertedRows;
            }
        }


        #region Private

        private SqlConnection getMainDbConn()
        {
            return new SqlConnection(_databaseConfig.MainConnectionString);
        }

        #endregion
    }
}
