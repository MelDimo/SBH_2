using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.dll.services.servicechannelsm
{
    public abstract class StateBase<T>
    {
        protected T automation;
        protected IEventSink eventSink;

        public StateBase(T automation, IEventSink eventSink)
        {
            if (automation == null || eventSink == null) throw new ArgumentNullException();

            this.automation = automation;
            this.eventSink = eventSink;
        }

        protected void castEvent(Event pEvent)
        {
            eventSink.CastEvent(pEvent);
        }
    }
}
