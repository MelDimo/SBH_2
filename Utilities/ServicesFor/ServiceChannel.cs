using com.sbh.dto.simpleobjects;
using com.sbh.dto.srv;
using com.sbh.srv.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.sbh.dll.services
{
    public class ServiceChannel
    {
        public enum CHANNELSTATE
        {
            CONNECTED,
            DISCONNECTED,
            UNINITIALISED
        }

        private EndpointAddress endpoint;
        private BroadcastorCallback bc;
        private DuplexChannelFactory<IBroadcastorService> dualFactory;

        private Dictionary<MSGTYPE, EventHandler> subscribers;

        private IBroadcastorService channel;

        private CHANNELSTATE curChannelState;

        /// <summary>
        /// Просматриваем доступность сервиса
        /// </summary>
        private BackgroundWorker bgwOnlineWatcher;

        public ServiceChannel()
        {
            if (dualFactory == null)
            {
                curChannelState = CHANNELSTATE.UNINITIALISED;

                subscribers = new Dictionary<MSGTYPE, EventHandler>();

                endpoint = new EndpointAddress("http://192.168.1.222:584/BroadcastorService");
                bc = new BroadcastorCallback();
                bc.SetHandler(this.HandleBroadcast);
                dualFactory = new DuplexChannelFactory<IBroadcastorService>(bc, new WSDualHttpBinding() { CloseTimeout = new TimeSpan(0, 0, 10) }, endpoint);
                channel = dualFactory.CreateChannel();

                bgwOnlineWatcher = new BackgroundWorker();
                bgwOnlineWatcher.WorkerReportsProgress = true;
                bgwOnlineWatcher.DoWork += BgwOnlineWatcher_DoWork;
                bgwOnlineWatcher.ProgressChanged += BgwOnlineWatcher_ProgressChanged;

                
            }
        }

        #region bgwOnlineWatcher Members Просматриваем доступность сервиса

        private void BgwOnlineWatcher_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Debug.Print($"curChannelState: {curChannelState}");
        }

        private void BgwOnlineWatcher_DoWork(object sender, DoWorkEventArgs e)
        {
            bool result = false;

            while (true)
            {
                try
                {
                    if (curChannelState == CHANNELSTATE.UNINITIALISED)
                        RegisterClient();
                    else
                     result = channel.CheckAvaliable();
                }
                catch (Exception exc)
                {
                    ;
                }

                if (!result) curChannelState = CHANNELSTATE.DISCONNECTED;
                else curChannelState = CHANNELSTATE.CONNECTED;
                (sender as BackgroundWorker).ReportProgress(0);
                Thread.Sleep(5000);
            }
        }

        #endregion

        private void HandleBroadcast(object sender, EventArgs e)
        {
            Msg eventData = (Msg)sender;

            subscribers[eventData.MsgTypeOut].Invoke(sender, e);
        }

        public MSG RegisterClient()
        {
            MSG result = new MSG() { Code = CODES.SUCCESS, Text = string.Empty, Obj = null };
            
            try
            {
                Msg msg = channel.RegisterClient(new Msg() { ClientName = "Client_1", GUID = new Guid() });

                curChannelState = CHANNELSTATE.CONNECTED;
            }
            catch (Exception exc)
            {
                curChannelState = CHANNELSTATE.UNINITIALISED;

                result.Code = CODES.ERROR;
                result.Text = "Неудалось зарегистрироваться на сервере.";
                result.Obj = exc;
            }

            bgwOnlineWatcher.RunWorkerAsync();

            return result;
        }

        /// <summary>
        /// Подписываемся на отслеживание изменений
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="eventHandler"></param>
        public void Subscribe(MSGTYPE msgType, EventHandler eventHandler)
        {
            //subscribers.Keys.Contains(msgType)

            Msg result = channel.Subscribe(new Msg() { ClientName = "client_1", GUID = new Guid(), MsgTypeIn = msgType });

            if (result?.MsgStatus == MSGSTATUS.SUCCESS) subscribers.Add(msgType, eventHandler);

        }

        /// <summary>
        /// Отписываемся от отслеживания изменений
        /// </summary>
        /// <param name="msgType"></param>
        public void UnScribe(MSGTYPE msgType)
        {
            Msg result = channel.UnSubscribe(new Msg() { ClientName = "client_1", GUID = new Guid(), MsgTypeIn = msgType });

            if (result?.MsgStatus == MSGSTATUS.SUCCESS) subscribers.Remove(msgType);
        }
    }
}
