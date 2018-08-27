using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.dll.services.servicechannelsm
{
    public interface IConnection 
    {
        void Connect();
        void Disconnect();
        int Receive();
        void Send(byte[] value);
    }
}
