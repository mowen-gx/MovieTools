using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MovieLink.Data.MsSql;
using MovieLink.Model;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.HtmlParser.MTime
{
    public class MovieInfoMenuParser : IMenuLinkParse
    {
        public List<string> GetLinks(string url)
        {
            List<string> links = new List<string>();
            try
            {
                HtmlWeb webClient = new HtmlWeb();
                HtmlDocument doc = webClient.Load(url);
                HtmlNode cNode = doc.GetElementbyId("nationPickerRegion");
                List<Country> contries = new List<Country>();
                if (cNode != null)
                {
                    HtmlNodeCollection collection = cNode.SelectNodes(cNode.XPath + "//ul/li/a");
                    if (collection != null)
                    {
                        foreach (HtmlNode htmlNode in collection)
                        {
                            contries.Add(new Country() { Name = htmlNode.InnerText.Trim() });
                            if (htmlNode.Attributes["cvalue"] != null)
                            {
                                string cate = string.Format("http://service.channel.mtime.com/service/search.mcs?Ajax_CallBack=true&Ajax_CallBackType=Mtime.Channel.Pages.SearchService&Ajax_CallBackMethod=SearchMovieByCategory&Ajax_CrossDomain=0&Ajax_RequestUrl=http%3A%2F%2Fmovie.mtime.com%2Fmovie%2Fsearch%2Fsection%2F&t=201362021522652913&Ajax_CallBackArgument0=&Ajax_CallBackArgument1=0&Ajax_CallBackArgument2={0}&Ajax_CallBackArgument3=0&Ajax_CallBackArgument4=0&Ajax_CallBackArgument5=0&Ajax_CallBackArgument6=0&Ajax_CallBackArgument7=0&Ajax_CallBackArgument8=&Ajax_CallBackArgument9=0&Ajax_CallBackArgument10=0&Ajax_CallBackArgument11=0&Ajax_CallBackArgument12=0&Ajax_CallBackArgument13=0&Ajax_CallBackArgument14=1&Ajax_CallBackArgument15=0&Ajax_CallBackArgument16=1&Ajax_CallBackArgument17=4&Ajax_CallBackArgument18=1&Ajax_CallBackArgument19=0", htmlNode.Attributes["cvalue"].ToString().Trim());
                                links.Add(cate);
                            }
                        }
                    }
                }

                HtmlNode tNode = doc.GetElementbyId("typePickerRegion");
                List<Model.Type> types = new List<Model.Type>();
                if (tNode != null)
                {
                    HtmlNodeCollection collection = tNode.SelectNodes(tNode.XPath + "//ul/li/a");
                    if (collection != null)
                    {
                        foreach (HtmlNode htmlNode in collection)
                        {
                            types.Add(new Model.Type() { Name = htmlNode.InnerText.Trim() });
                        }
                    }
                }

                HtmlNode lNode = doc.GetElementbyId("languagePickerRegion");
                List<Language> languages = new List<Language>();
                if (tNode != null)
                {
                    HtmlNodeCollection collection = lNode.SelectNodes(lNode.XPath + "//ul/li/a");
                    if (collection != null)
                    {
                        foreach (HtmlNode htmlNode in collection)
                        {
                            languages.Add(new Language() { Name = htmlNode.InnerText.Trim() });
                        }
                    }
                }

                MovieData movieData = new MovieData();
                movieData.AddTypes(types);
                movieData.AddCountries(contries);
                movieData.AddLanguages(languages);
            }
            catch (Exception)
            {
            }
            return links;
        }
    }
}
