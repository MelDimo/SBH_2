using com.sbh.dto.srv;
using com.sbh.srv.interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.dll.services
{
    public class ServiceChannel
    {

        private EndpointAddress endpoint;
        private BroadcastorCallback bc;
        private DuplexChannelFactory<IBroadcastorService> dualFactory;

        private Dictionary<MSGTYPE, EventHandler> subscribers;

        private IBroadcastorService channel;

        public ServiceChannel()
        {
            if (dualFactory == null)
            {
                subscribers = new Dictionary<MSGTYPE, EventHandler>();

                OpenChannel();
            }
        }

        private void HandleBroadcast(object sender, EventArgs e)
        {
            Msg eventData = (Msg)sender;

            subscribers[eventData.MsgTypeOut].Invoke(sender, e);
        }

        private void OpenChannel()
        {
            endpoint = new EndpointAddress("http://192.168.1.222:584/BroadcastorService");
            bc = new BroadcastorCallback();
            bc.SetHandler(this.HandleBroadcast);
            dualFactory = new DuplexChannelFactory<IBroadcastorService>(bc, new WSDualHttpBinding(), endpoint);
            channel = dualFactory.CreateChannel();

            Msg result = channel.RegisterClient(new Msg() { ClientName = "Client_1", GUID = new Guid() });

            Debug.Print($"{result.MsgStatus}");

            channel.NotifyServer(new Msg() { ClientName = "client_1", GUID = new Guid(), MsgTypeIn = MSGTYPE.WATCHONLINE });


        }

        public void Subscribe(MSGTYPE msgType, EventHandler eventHandler)
        {
            subscribers.Add(msgType, eventHandler);
        }
    }
}
