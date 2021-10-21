using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworksViewerApp.Models
{
    public class STIGModel
    {
        public int id { get; set; }
        public string target { get; set; }

        public int cciid { get; set; }

        public string title { get; set; }

        public string groupidno { get; set; }

        public string descr { get; set; }
        List<STIGRuleModel> rules { get; set; }
    }
}
