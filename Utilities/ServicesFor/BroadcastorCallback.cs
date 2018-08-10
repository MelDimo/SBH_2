using com.sbh.srv.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.sbh.dll.services
{
    public class BroadcastorCallback : IBroadcastorCallBack
    {
        private SynchronizationContext synchronizationContext = AsyncOperationManager.SynchronizationContext;

        private EventHandler broadcastorCallBackHandler;
        public void SetHandler(EventHandler handler)
        {
            broadcastorCallBackHandler = handler;
        }

        private void OnBroadcast(object eventData)
        {
            broadcastorCallBackHandler.Invoke(eventData, null);
        }

        #region IBroadcastorCallBack Members

        public void BroadcastToClient(EventDataType eventData)
        {
            synchronizationContext.Post(new SendOrPostCallback(OnBroadcast), eventData);
        }

        #endregion
    }
}
