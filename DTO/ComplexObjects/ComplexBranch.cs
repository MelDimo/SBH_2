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
        public int Id { get; set; }
        public string Name { get; set; }
        public int RefStatus { get; set; }
        public ObservableCollection<Unit> Units { get; set; }
    }
}
