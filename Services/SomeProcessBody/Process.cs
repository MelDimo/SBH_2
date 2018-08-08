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
        List<IProcessCallback> processCallbacks = new List<IProcessCallback>();
        

        //public async void TaskProcess()
        //{
        //    await Task.Run() => { }

        //                for (int i = 0; i <= 100; i++)
        //    {
        //        Thread.Sleep(50);
        //        OperationContext.Current.GetCallbackChannel<IProcessCallback>().TaskProgress(i);
        //    }

        //}

        async void IProcess.TaskProcess()
        {
            IProcessCallback callback = OperationContext.Current.GetCallbackChannel<IProcessCallback>();
            if (!processCallbacks.Contains(callback)) processCallbacks.Add(callback);

            await TaskProcessDoWork();
        }

        private async Task TaskProcessDoWork()
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(50);
                foreach (IProcessCallback callback in processCallbacks)
                {
                    callback.TaskProgress(i);
                }
            }
        }
    }
}
