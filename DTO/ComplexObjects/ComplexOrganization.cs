using SimpleObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexObjects
{
    public class ComplexOrganization
    {
        public Organization Organization { get; set; }
        public ObservableCollection<ComplexBranch> Branches { get; set; }
    }
}
