using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLink.Model;

namespace MovieLink.Service.Interface.HtmlParser
{
    public interface IDetaiParse
    {
        Movie GetDetail(string link);
    }
}
