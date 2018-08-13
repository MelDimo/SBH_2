using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.dto.srv
{
    /// <summary>
    /// Типы сообщений для взаимодействия клиента и сервера
    /// </summary>
    [DataContract]
    public enum MSGTYPE
    {
        [EnumMember]
        WATCHONLINE                         // Опрос сервера на предмет доступности
    }

    [DataContract]
    public enum MSGSTATUS
    {
        [EnumMember]
        SUCCESS,                            // Успех
        [EnumMember]
        ERROR,                              // Ошибка
        [EnumMember]
        ABORT,                              // Отмена
        [EnumMember]
        UNDEFINED                           // Не определено
    }        

    [DataContract]
    public class Msg
    {
        /// <summary>
        /// Уникальный идентивикатор сообщения (передается клиентом)
        /// </summary>
        [DataMember]
        public Guid GUID { get; set; }

        /// <summary>
        /// Идентификатор клиента (передается клиентом)
        /// </summary>
        [DataMember]
        public string ClientName { get; set; }

        /// <summary>
        /// Тип входящего сообщения (от клиента)
        /// </summary>
        [DataMember]
        public MSGTYPE MsgTypeIn { get; set; }

        /// <summary>
        /// Тип исходящего сообщения (к клиенту)
        /// </summary>
        [DataMember]
        public MSGTYPE MsgTypeOut { get; set; }

        /// <summary>
        /// Статус исходящего сообщения
        /// </summary>
        [DataMember]
        public MSGSTATUS MsgStatus { get; set; }

        /// <summary>
        /// Объект при необходимости (в обе стороны)
        /// </summary>
        [DataMember]
        public object Obj { get; set; }

        /// <summary>
        /// Объект собержащий ошибку при необходимости (в обе стороны)
        /// </summary>
        [DataMember]
        public object Error { get; set; }
    }
}
