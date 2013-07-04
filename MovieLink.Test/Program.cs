using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MovieLink.Data.MsSql;
using MovieLink.Service.Impl.HtmlParser;
using MovieLink.Service.Impl.HtmlParser.Dy2018;
using MovieLink.Service.Impl.HtmlParser.MTime;
using MovieLink.Service.Impl.HtmlParser.Piaohua;

namespace MovieLink.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieInfoMenuParser parser = new MovieInfoMenuParser();

          //parser.GetLinks("http://service.channel.mtime.com/service/search.mcs?Ajax_CallBack=true&Ajax_CallBackType=Mtime.Channel.Pages.SearchService&Ajax_CallBackMethod=SearchMovieByCategory&Ajax_CrossDomain=0&Ajax_RequestUrl=http%3A%2F%2Fmovie.mtime.com%2Fmovie%2Fsearch%2Fsection%2F&t=201362021522652913&Ajax_CallBackArgument0=&Ajax_CallBackArgument1=0&Ajax_CallBackArgument2=275&Ajax_CallBackArgument3=0&Ajax_CallBackArgument4=0&Ajax_CallBackArgument5=0&Ajax_CallBackArgument6=0&Ajax_CallBackArgument7=0&Ajax_CallBackArgument8=&Ajax_CallBackArgument9=0&Ajax_CallBackArgument10=0&Ajax_CallBackArgument11=0&Ajax_CallBackArgument12=0&Ajax_CallBackArgument13=0&Ajax_CallBackArgument14=1&Ajax_CallBackArgument15=0&Ajax_CallBackArgument16=1&Ajax_CallBackArgument17=4&Ajax_CallBackArgument18=1&Ajax_CallBackArgument19=0");

          parser.GetLinks("http://movie.mtime.com/movie/search/section/");
        }
    }
}
