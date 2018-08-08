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
        [OperationContract(IsOneWay = true)]
        void TaskProcess();
    }

    public interface IProcessCallback
    {
        [OperationContract(IsOneWay = true)]
        void TaskProgress(int percentDone);
    }
}
