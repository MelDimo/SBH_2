using com.sbh.srv.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///     Писал отсюда
///     https://www.codeproject.com/Articles/596287/Broadcasting-Events-with-a-Duplex-WCF-Service
/// </summary>

namespace com.sbh.srv.implementations
{
    [ServiceBehavior(Name = "BroadcastorService",
        InstanceContextMode = InstanceContextMode.Single, 
        ConcurrencyMode = ConcurrencyMode.Multiple
        )]
    public class BroadcastorService : IBroadcastorService
    {
        private static Dictionary<string, IBroadcastorCallBack> clients = new Dictionary<string, IBroadcastorCallBack>();
        private static object locker = new object();

        public bool RegisterClient(string clientName)
        {
            Console.WriteLine($"RegisterClient: {clientName}");

            lock (locker)
            {
                bool result = false;

                if (!String.IsNullOrEmpty(clientName))
                {
                    IBroadcastorCallBack callBack = OperationContext.Current.GetCallbackChannel<IBroadcastorCallBack>();

                    if (clients.Keys.Contains(clientName)) clients.Remove(clientName);

                    clients.Add(clientName, callBack);

                    result = true;

                }

                return result;
            }
        }

        public void NotifyServer(EventDataType eventData)
        {
            Console.WriteLine($"Client: {eventData.ClientName}; Message:{eventData.EventMessage}");

            lock (locker)
            {
                var inactiveClients = new List<string>();
                foreach (var client in clients)
                {
                    if (client.Key != eventData.ClientName)
                    {
                        try
                        {
                            client.Value.BroadcastToClient(eventData);
                        }
                        catch (Exception exc)
                        {
                            inactiveClients.Add(client.Key);
                        }
                    }
                }

                if (inactiveClients.Count > 0)
                {
                    foreach (var client in inactiveClients)
                    {
                        clients.Remove(client);
                    }
                }
            }
        }
    }
}
