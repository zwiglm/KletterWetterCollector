using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.DataAccess.ApiCommands
{
    public class UpdBaseDataCmd : BaseCommand
    {
        public DateTime _created;
        public decimal _temperature;
        public short _pwrWarn;

        public UpdBaseDataCmd(DateTime created, decimal temperature, short pwrWarn)
        {
            this._created = created;
            this._temperature = temperature;
            this._pwrWarn = pwrWarn;
        }
    }
}
