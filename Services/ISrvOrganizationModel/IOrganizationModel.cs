using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ISrvOrganizationModel
{
    [ServiceContract]
    public interface IOrganizationModel
    {
        [OperationContract]
        ObservableCollection<ComplexObjects.ComplexOrganization> GetOrganization();
    }
}
