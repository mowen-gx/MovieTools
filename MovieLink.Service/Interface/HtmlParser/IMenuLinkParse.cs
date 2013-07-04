using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLink.Service.Interface.HtmlParser
{
    public interface IMenuLinkParse
    {
        List<string> GetLinks(string url);
    }
}
