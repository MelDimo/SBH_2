using com.sbh.dto.complexobjects;
using com.sbh.dto.simpleobjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.srv.interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallBack))]
    public interface IOrganizationModel
    {
        [OperationContract(IsOneWay = true)]
        void GetOrganization();

        [OperationContract(IsOneWay = true)]
        void AddOrganization(Organization organization);
    }
}
