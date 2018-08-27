using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.dll.services.servicechannelsm
{
    public class DisconnectedState<T> : StateBase<T>, IConnection where T : IConnection
    {
        public static readonly Event CONNECT = new Event("CONNECT");
        public static readonly Event ERROR = new Event("ERROR");

        protected readonly Socket socket;

        public DisconnectedState(T automation, IEventSink eventSink, Socket socket) : base(automation, eventSink)
        {
            this.socket = socket;
        }
        #region IConnection Members

        public void Connect()
        {
            try
            {
                socket.Connect("localhost", 584);
            }
            catch (IOException exc)
            {
                throw exc;
            }
        }

        public void Disconnect() { }

        public int Receive()
        {
            throw new IOException("Connection is closed. (Recive)");
        }

        public void Send(byte[] value)
        {
            throw new IOException("Connection is closed. (Send)");
        }

        #endregion
    }
}
