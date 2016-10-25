using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTGraph1 {
    public class DTDFrame {
        public string name { get; set; }
    }

    public class DTDSlot {
        public string name { get; set; }
    }

    public class DTDTerm {
        public string value { get; set; }
    }

    public class DTDClarification {
        public string value;
    }
}
