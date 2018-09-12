using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Politexniki_Client.SystemMessage
{
    class SystemMessageHandler
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        protected SystemMessageHandler()
        {

        }

        #endregion

        #region Properties

        private static SystemMessageHandler _instance;


        #endregion

        #region Methods

        #region Singleton

        /// <summary>
        /// Singleton pattern
        /// </summary>
        /// <returns></returns>
        public static SystemMessageHandler GetInstance()
        {
            return _instance ?? (_instance = new SystemMessageHandler());
        }

        #endregion

        /// <summary>
        /// Notify listeners of system events
        /// </summary>
        /// <param name="message"></param>
        public void NotifySystemMessage(SystemMessageClass message)
        {
            OnSystemMessage?.Invoke(message);
        }

        #endregion

        #region Event

        public delegate void NotifyMessageDelegate(SystemMessageClass message);

        public event NotifyMessageDelegate OnSystemMessage;

        public void SubscribeNotifySystemMessage(NotifyMessageDelegate target)
        {
            OnSystemMessage += target;
        }

        public void UnSubscribeNotifySystemMessage(NotifyMessageDelegate target)
        {
            OnSystemMessage -= target;
        }

        #endregion
    }
}
