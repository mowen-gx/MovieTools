using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLink.Win
{
    public class MovieMsg
    {
        public int CurrentGetWebDetailLink { get; set; }
        public int CurrentDoneDetailLink { get; set; }
        public int CurrentDbNotParseDetailLink { get; set; }
        public int CurrentDoneMovie { get; set; }
        public List<string> ParserMsgs { get; set; }
        public List<string> DbMsgs { get; set; }
    }
}
