using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace com.sbh.dto.complexobjects
{
    public class ComplexOrganization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RefStatus { get; set; }

        [XmlElement("ArrayOfComplexBranch", typeof(ObservableCollection<ComplexBranch>))]
        public ObservableCollection<ComplexBranch> Branches { get; set; }
    }
}
