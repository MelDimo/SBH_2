using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.dll.services.servicechannelsm
{
    public interface IEventSink
    {
        void CastEvent(Event pEvent);
    }
}
