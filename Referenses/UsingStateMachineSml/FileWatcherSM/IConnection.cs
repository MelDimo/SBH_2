using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingStateMachineSml.FileWatcherSM
{
    public interface IConnection
    {
        void Connect();
        void Dicsonnect();
    }
}
