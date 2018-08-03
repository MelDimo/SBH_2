using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SomeProcess
{
    [ServiceContract(CallbackContract = typeof(IProcessCallback))]
    public interface IProcess
    {
        [OperationContract]
        void TaskProcess();

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginProcessAsync(AsyncCallback callback, object state);
        void EndTaskProcessAsync(IAsyncResult ar);
    }

    public interface IProcessCallback
    {
        [OperationContract]
        void TaskProgress(int percentDone);
    }
}
