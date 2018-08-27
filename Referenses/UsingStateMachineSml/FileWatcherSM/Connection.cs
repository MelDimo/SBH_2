using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingStateMachineSml.FileWatcherSM
{
    public class Connection
    {
        Action<string> action;

        public Connection(Action<string> action)
        {
            ConnectAsyc();
            this.action = action;
        }

        public async void ConnectAsyc()
        {
            await Task.Delay(10000);

            action("ConnectAsyc");
        }


    }
}
