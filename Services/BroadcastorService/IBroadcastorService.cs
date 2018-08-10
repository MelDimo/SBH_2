using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.srv.interfaces
{
    [DataContract]
    public class EventDataType
    {
        [DataMember]
        public string ClientName { get; set; }

        [DataMember]
        public string EventMessage { get; set; }
    }

    public interface IBroadcastorCallBack
    {
        [OperationContract(IsOneWay = true)]
        void BroadcastToClient(EventDataType eventData);
    }

    [ServiceContract(CallbackContract = typeof(IBroadcastorCallBack))]
    public interface IBroadcastorService
    {
        /// <summary>
        /// Регистрация клиента на сервере
        /// </summary>
        /// <param name="clientName">Имя клиента</param>
        [OperationContract(IsOneWay = false)]
        bool RegisterClient(string clientName);

        /// <summary>
        /// Уведомление службы о событии на клиенте
        /// </summary>
        /// <param name="eventData">Событие</param>
        [OperationContract(IsOneWay = true)]
        void NotifyServer(EventDataType eventData);
    }
}
