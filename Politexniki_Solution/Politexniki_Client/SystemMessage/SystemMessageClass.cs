using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Politexniki_Client.SystemMessage
{
    class SystemMessageClass
    {
        public enum ErrorType { Store = 0}
        public enum MessageType { None = 0, Info = 1, Warning = 2, Error = 3, Always = 5 }

        public bool Success { get; set; }
        public string Source { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public ErrorType Errortype { get; set; }

        /// <summary>
        /// Messagetype
        /// </summary>
        public MessageType Messagetype { get; set; }
    }
}
