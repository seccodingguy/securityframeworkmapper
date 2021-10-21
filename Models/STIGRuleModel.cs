using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworksViewerApp.Models
{
    public class STIGRuleModel
    {
        public int id { get; set; }

        public string ruleidno { get; set; }
        public int weight { get; set; }
        public string severity { get; set; }
        public string version { get; set; }
        public string descr { get; set; }

        public STIGRuleFix fixdetail { get; set; }

        public STIGRuleCheck checkdetail { get; set; }
    }
}
