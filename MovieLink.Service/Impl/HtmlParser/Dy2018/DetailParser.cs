using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using MovieLink.Model;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.HtmlParser.Dy2018
{
    public class DetailParser : IDetaiParse
    {
        public Movie GetDetail(string link)
        {
            Movie movie = null;
            try
            {
                string name = "";
                string cover = "";
                string sumary = "";
                List<string> downLinks = new List<string>();
                List<string> othorNames = new List<string>();
                HtmlDocument doc = ParserUtil.GetWeb(link);
                List<HtmlNode> nodes = ParserUtil.GetNodesByClass(doc.DocumentNode, "bd3r");
                if (nodes != null)
                {
                    if (nodes.Count > 0)
                    {
                        HtmlNode titleNode = nodes[0].SelectSingleNode(nodes[0].XPath + "/div/div[@class=\"title_all\"]");
                        if (titleNode != null)
                        {
                            HtmlNode nameNode = titleNode.SelectSingleNode(titleNode.XPath + "/h1/font");
                            if (nameNode != null)
                            {
                                name = nameNode.InnerText.Trim();
                                if (name.Contains("《") && name.Contains("》"))
                                {
                                    name = name.Substring(name.IndexOf("《") + 1,
                                                          (name.IndexOf("》") - name.IndexOf("《") - 1));
                                    if (name.Contains("/"))
                                    {
                                        string[] names = name.Split('/');
                                        if (names.Length > 0)
                                        {
                                            name = names[0];
                                            othorNames.AddRange(names);
                                        }
                                    }
                                    else
                                    {
                                        othorNames.Add(name);
                                    }
                                }
                            }
                        }

                        HtmlNode zoomNode = doc.GetElementbyId("Zoom");
                        if (zoomNode != null)
                        {
                            HtmlNodeCollection imgNodes = zoomNode.SelectNodes(zoomNode.XPath + "/span/td/p/img");
                            if (imgNodes != null)
                            {
                                if (imgNodes.Count > 0)
                                {
                                    cover = imgNodes[0].Attributes["src"].Value.Trim();
                                }
                            }

                            HtmlNode sumaryNode = zoomNode.SelectSingleNode(zoomNode.XPath + "/span/td/p");
                            if (sumaryNode != null)
                            {
                                sumary = sumaryNode.OuterHtml.Trim();
                            }
                        }


                        HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//td[@bgcolor=\"#fdfddf\"]");
                        if (collection != null)
                        {
                            foreach (HtmlNode htmlNode in collection)
                            {
                                GetMovieDownLinks(htmlNode, ref downLinks);
                            }
                        }

                        if (!string.IsNullOrEmpty(name))
                        {
                            movie = new Movie();
                            movie.Name = name;
                            movie.MainPic = cover;
                            movie.Summary = sumary;
                            movie.OtherNames = othorNames;
                            movie.DownloadLinks = downLinks;
                            movie.Source = link;
                            movie.SourceName = "电影天堂";
                        }
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
