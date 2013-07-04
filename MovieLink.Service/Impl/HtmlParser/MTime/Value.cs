using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLink.Service.Impl.HtmlParser.MTime
{
    public class Value
    {
        public bool vcodeValid { get; set; }
        public int totalCount { get; set; }
        public string listHTML { get; set; }
        public string error { get; set; }
    }
}
