using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.dll.services.servicechannelsm
{
    public class ConnectedState<T> : StateBase<T>, IConnection where T:IConnection
    {
        public static readonly Event DISCONNECT = new Event("DISCONNECT");
        public static readonly Event ERROR = new Event("ERROR");

        protected readonly Socket socket;
        public static byte[] ReciveData;

        public ConnectedState(T automation, IEventSink eventSink, Socket socket) : base(automation, eventSink)
        {
            this.socket = socket;
        }

        #region IConnection Members

        public void Connect() { }

        public void Disconnect()
        {
            try
            {
                socket.Disconnect(true);
            }
            finally
            {
                eventSink.CastEvent(DISCONNECT);
            }
        }

        public int Receive()
        {
            try
            {
                ReciveData = null;
                return socket.Receive(ReciveData);
            }
            catch (IOException exc)
            {
                eventSink.CastEvent(ERROR);
                throw exc;
            }
        }

        public void Send(byte[] value)
        {
            try
            {
                socket.Send(value);
            }
            catch (IOException exc)
            {
                eventSink.CastEvent(ERROR);
                throw exc;
            }
        }

        #endregion

    }
}
