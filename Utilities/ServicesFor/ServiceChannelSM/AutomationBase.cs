using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.dll.services.servicechannelsm
{
    public abstract class AutomationBase<T> : IEventSink
    {
        protected T state;
        private Dictionary<T, Dictionary<Event, T>> edges = new Dictionary<T, Dictionary<Event, T>>();

        protected void addEdge(T source, Event pEvent, T target)
        {
            Dictionary<Event, T> row = edges[source];
            if (row == null)
            {
                row = new Dictionary<Event, T>();
                edges.Add(source, row);
            }
            row.Add(pEvent, target);
        }

        public void CastEvent(Event pEvent)
        {
            try
            {
                state = edges[state][pEvent];
            }
            catch (Exception exc)
            {
                throw new Exception("Edge is not defined");
            }
        }
    }
}
