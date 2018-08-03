using SomeProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SomeProcessBody
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class Process : IProcess
    {
        public IAsyncResult BeginProcessAsync(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public void EndTaskProcessAsync(IAsyncResult ar)
        {
            throw new NotImplementedException();
        }

        public void TaskProcess()
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(50);
                OperationContext.Current.GetCallbackChannel<IProcessCallback>().TaskProgress(i);
            }
        }

        
    }
}
