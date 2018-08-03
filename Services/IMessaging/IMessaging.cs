using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.srv.interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallBack))]
    public interface IMessaging
    {

    }
}
