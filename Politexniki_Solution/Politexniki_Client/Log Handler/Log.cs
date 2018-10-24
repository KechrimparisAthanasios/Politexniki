using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Politexniki_Client.Log_Handler
{
    public class Log
    {
        #region Properties

        public int LogId { get; set; }
        public string LogClass { get; set; }
        public string LogMethod { get; set; }
        public string LogMessage { get; set; }
        public DateTime LogTime { get; set; }

        #endregion

        #region Constructor

        public Log()
        {
        }

        #endregion

    }
}
