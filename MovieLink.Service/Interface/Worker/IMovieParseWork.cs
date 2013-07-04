using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Interface.Worker
{
    public interface IMovieParseWork
    {
        IDetaiParse GetDetaiParser();
        string GetMovieType();
        void Run();
    }
}
