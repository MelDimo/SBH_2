using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace com.sbh.dto.simpleobjects
{
    
    public class MSG
    {
    
        public CODES Code { get; set; }
    
        public string Text { get; set; }
    
        public object Obj { get; set; }
    }
}
