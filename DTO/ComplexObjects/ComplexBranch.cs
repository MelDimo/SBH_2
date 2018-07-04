using SimpleObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexObjects
{
    public class ComplexBranch
    {
        public Branch Branch { get; set; }
        public ObservableCollection<Unit> Units { get; set; }
    }
}
