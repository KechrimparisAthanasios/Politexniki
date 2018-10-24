using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Politexniki_Client.Log_Handler
{
    public class LogHandling
    {
        #region Singleton

        private static readonly LogHandling instance = new LogHandling();
        static LogHandling() { }

        private LogHandling() { }

        public static LogHandling Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Save the log in the SQLite
        /// </summary>
        /// <param name="logClass"></param>
        /// <param name="logMethod"></param>
        /// <param name="message"></param>
        /// <param name="logDate"></param>
        public void StoreLog(string logClass, string logMethod, string message, DateTime logDate)
        {
            Log log = new Log()
            {
                LogClass = logClass,
                LogMethod = logMethod,
                LogMessage = message,
                LogTime = DateTime.Now
            };

            SQLite.SqLiteHandling.Instance.StoreLog(log);
        }

        #endregion
    }
}
