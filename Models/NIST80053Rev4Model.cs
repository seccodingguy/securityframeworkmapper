using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworksViewerApp.Models
{
    public class NIST80053Model
    {
        public int id { get; set; }
        public string number { get; set; }
        public string title { get; set; }
        public string impact { get; set; }
        public string priority { get; set; }
        public string descr { get; set; }

        public string guidance { get; set; }

        public string nistfamilyname { get; set; }

        public int revisionnumber {get; set;}
    }
}
