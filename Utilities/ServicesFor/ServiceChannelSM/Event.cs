using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.dll.services.servicechannelsm
{
    public class Event
    {
        private readonly string name;

        public Event(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException();
            this.name = name;
        }

        public string Name { get { return name; } }
    }
}
