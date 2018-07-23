using ComplexObjects;
using ISrvOrganizationModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrvOrganizationModel
{
    public class OrganizationModel : IOrganizationModel
    {
        readonly DBAccess dbAccess;

        public OrganizationModel()
        {
            dbAccess = new DBAccess();
        }

        public ObservableCollection<ComplexOrganization> GetOrganization()
        {
            return dbAccess.GetOrganization();
        }
    }
}
