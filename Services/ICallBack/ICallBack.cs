using com.sbh.dto.simpleobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.srv.interfaces
{
    public interface ICallBack
    {
        [OperationContract(IsOneWay = true)]
        void FromOrganizationModel(MSG message);
    }
}
