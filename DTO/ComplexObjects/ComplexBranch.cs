using com.sbh.dto.simpleobjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace com.sbh.dto.complexobjects
{
    public class ComplexBranch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RefStatus { get; set; }

        [XmlElement("ArrayOfUnit", typeof(ObservableCollection<Unit>))]
        public ObservableCollection<Unit> Units { get; set; }
    }
}
