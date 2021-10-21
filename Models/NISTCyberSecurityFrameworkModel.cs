using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworksViewerApp.Models
{
    public class NISTCyberSecurityFrameworkModel
    {
        public int id { get; set; }
        public string functionname { get; set; }
        public string categoryname { get; set; }
        public string categorynameid { get; set; }
        public string categorynamedescr { get; set; }
        public string subcategoryid { get; set; }

        public string subcategorydescr { get; set; }

        public List<NIST80053Model> NISTControls { get; set; }

        public List<FedRAMPModel> FedRAMPControls { get; set; }

        public List<CCIModel> CCIControls { get; set; }
    }
}
