using System.Collections.Generic;

namespace MovieLink.MovieInfoWin
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
