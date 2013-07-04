using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MovieLink.Model;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.HtmlParser.Piaohua
{
    public class DetailParser : IDetaiParse
    {
        public Movie GetDetail(string link)
        {
            Movie movie = null;
            try
            {
                HtmlWeb webClient = new HtmlWeb();
                HtmlDocument doc = webClient.Load(link);
                HtmlNode node = doc.GetElementbyId("showdesc");
                if (node != null)
                {
                    HtmlNode img = node.SelectSingleNode(node.XPath + "//img");
                    if (img != null)
                    {
                        movie = new Movie();
                        movie.Source = link;
                        //movie.OtherName = "飘花电影";
                        movie.MainPic = img.Attributes["src"].Value.Trim();
                        string[] names = img.Attributes["alt"].Value.Trim().Split('/');
                        if (names.Length > 0)
                        {
                            movie.Name = names[0];
                            ParserMsg.SetMsg("读取电影" + movie.Name);
                            if (names.Length > 1)
                            {
                                movie.OtherNames.AddRange(names);
                            }
                        }
                    }
                }

                if (movie != null)
                {
                    HtmlNode showinfo = doc.GetElementbyId("showinfo");
                    if (showinfo != null)
                    {
                        HtmlNodeCollection tables = showinfo.SelectNodes(showinfo.XPath + "//table");
                        if (tables != null)
                        {
                            if (tables.Count > 0)
                            {
                                foreach (HtmlNode htmlNode in tables)
                                {
                                    List<string> downloadLinks = new List<string>();
                                    GetMovieDownLinks(htmlNode, ref downloadLinks);
                                    if (downloadLinks.Count > 0)
                                    {
                                        movie.DownloadLinks.AddRange(downloadLinks);
                                    }
                                }
                            }
                        }
                        movie.Summary = showinfo.InnerHtml;
                    }
                }

            }
            catch (Exception ex)
            {
                ParserMsg.SetMsg("错误:" + ex.Message + ex.StackTrace);
            }
            return movie;
        }

        private void GetMovieDownLinks(HtmlNode node, ref List<string> downloadLinks)
        {
            if (node != null)
            {
                if (node.Name.ToLower() == "a")
                {
                    if (node.Attributes["thunderhref"] != null)
                    {
                        string link = node.Attributes["thunderhref"].Value.Trim();
                        if (!downloadLinks.Contains(link))
                        {
                            downloadLinks.Add(link);
                        }
                    }
                    else
                    {
                        string link = node.Attributes["href"].Value.Trim();
                        if (!downloadLinks.Contains(link))
                        {
                            downloadLinks.Add(link);
                        }
                    }

                }
                else if (node.HasChildNodes)
                {
                    foreach (HtmlNode childNode in node.ChildNodes)
                    {
                        GetMovieDownLinks(childNode, ref downloadLinks);
                    }
                }
            }
        }
    }
}
