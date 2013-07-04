using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Interface.Worker
{
    public interface IPageParseWork
    {
        IPageLinkParse GetPageLinkParser();
        IMenuLinkParse GetMenuLinkParser();
        string GetUrl();
        void Run();
    }
}
