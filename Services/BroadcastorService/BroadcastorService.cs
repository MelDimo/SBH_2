using com.sbh.dto.srv;
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
        /// <summary>
        /// Словарь клиентов подписанных к серверу
        /// </summary>
        private static Dictionary<string, IBroadcastorCallBack> clients = new Dictionary<string, IBroadcastorCallBack>();

        /// <summary>
        /// Объект для блокировки потока
        /// </summary>
        private static object locker = new object();


        /// <summary>
        /// Регистрация клиента
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Msg RegisterClient(Msg msg)
        {
            Console.WriteLine($"RegisterClient: {msg.ClientName}");

            Msg result = new Msg() { ClientName = msg.ClientName, GUID = msg.GUID };

            lock (locker)
            {
                result = new Msg() { ClientName = msg.ClientName, GUID = msg.GUID };

                if (!String.IsNullOrEmpty(msg.ClientName))
                {
                    IBroadcastorCallBack callBack = OperationContext.Current.GetCallbackChannel<IBroadcastorCallBack>();

                    if (clients.Keys.Contains(msg.ClientName)) clients.Remove(msg.ClientName);

                    clients.Add(msg.ClientName, callBack);

                    result.MsgStatus = MSGSTATUS.SUCCESS;
                }
            }

            return result;

        }

        /// <summary>
        /// Метод подтверждает присутствие сервера
        /// </summary>
        /// <returns></returns>
        public bool CheckAvaliable()
        {
            return true;
        }

        /// <summary>
        /// Клиент информирует сервер
        /// </summary>
        /// <param name="eventData"></param>
        public void NotifyServer(Msg eventData)
        {
            Console.WriteLine($"Client: {eventData.ClientName}; Message:{eventData.Obj?.ToString()}");

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

        /// <summary>
        /// Клиент информирует сервер асинхронно
        /// </summary>
        /// <param name="eventData"></param>
        public async void NotifyServerAsync(Msg eventData)
        {
            Console.WriteLine($"Client: {eventData.ClientName}; Message:{eventData.Obj?.ToString()}");

            //lock (locker)
            //{
                var inactiveClients = new List<string>();
                foreach (var client in clients)
                {
                    if (client.Key != eventData.ClientName)
                    {
                        try
                        {
                            await Task.Run(() => client.Value.BroadcastToClient(eventData));
                            
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
            //}
        }

        /// <summary>
        /// Клиент подписывается на прослушивание изменения
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Msg Subscribe(Msg msg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Клиент отписывается от прослушивания изменения
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Msg UnSubscribe(Msg msg)
        {
            throw new NotImplementedException();
        }
    }
}
