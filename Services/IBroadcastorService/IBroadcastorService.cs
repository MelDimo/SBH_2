using com.sbh.dto.srv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.srv.interfaces
{
    public interface IBroadcastorCallBack
    {
        [OperationContract(IsOneWay = true)]
        void BroadcastToClient(Msg eventData);
    }

    [ServiceContract(CallbackContract = typeof(IBroadcastorCallBack))]
    public interface IBroadcastorService
    {
        /// <summary>
        /// Регистрация клиента на сервере
        /// </summary>
        /// <param name="clientName">Имя клиента</param>
        [OperationContract(IsOneWay = false)]
        Msg RegisterClient(Msg msg);

        /// <summary>
        /// Подписываемся на события
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [OperationContract(IsOneWay = false)]
        Msg Subscribe(Msg msg);

        /// <summary>
        /// Отписываемся от события
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [OperationContract(IsOneWay = false)]
        Msg UnSubscribe(Msg msg);

        /// <summary>
        /// Уведомление службы о событии на клиенте
        /// </summary>
        /// <param name="eventData">Событие</param>
        [OperationContract(IsOneWay = true)]
        void NotifyServer(Msg eventData);
    }


}
