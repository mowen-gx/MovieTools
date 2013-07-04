using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using MovieLink.Model;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.HtmlParser.MTime
{
    public class MovieInfoDetailParser : IDetaiParse
    {
        public Movie GetDetail(string link)
        {
            MovieInfo movie = null;
            try
            {
                string name = "";
                string era = "";
                string cover = "";
                Character director = null;
                List<Character> actors = new List<Character>();
                List<Language> languagea = new List<Language>();
                List<Model.Type> types = new List<Model.Type>();
                List<Country> countrys = new List<Country>();
                List<string> othorNames = new List<string>();
                HtmlDocument doc = ParserUtil.GetWeb(link);
                List<HtmlNode> nodes = ParserUtil.GetNodesByClass(doc.DocumentNode, "movie_film_nav normal pl9 pr15","h1");
                if (nodes != null)
                {
                    if (nodes.Count > 0)
                    {
                        HtmlNodeCollection titleNodes = nodes[0].SelectNodes(nodes[0].XPath + "/span");
                        if (titleNodes != null)
                        {
                            foreach (HtmlNode titleNode in titleNodes)
                            {
                                if (titleNode != null)
                                {
                                    name = titleNode.InnerText.Trim();
                                    if (!string.IsNullOrEmpty(name))
                                    {
                                        if (name.Contains("《") && name.Contains("》"))
                                        {
                                            name = name.Substring(name.IndexOf("《") + 1,
                                                                  (name.IndexOf("》") - name.IndexOf("《") - 1));
                                        }

                                        if (name.Contains("/"))
                                        {
                                            string[] names = name.Split('/');
                                            if (names.Length > 0)
                                            {
                                                foreach (string s in names)
                                                {
                                                    if (!string.IsNullOrEmpty(s))
                                                    {
                                                        othorNames.Add(s);
                                                    }
                                                }

                                            }
                                        }
                                        else if (name.Contains(","))
                                        {
                                            string[] names = name.Split(',');
                                            if (names.Length > 0)
                                            {
                                                foreach (string s in names)
                                                {
                                                    if (!string.IsNullOrEmpty(s))
                                                    {
                                                        othorNames.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            othorNames.Add(name);
                                        }
                                    }
                                }
                            }
                        }

                        if (othorNames.Count > 0)
                        {
                            name = othorNames[0].Trim();
                        }

                        HtmlNode eraNode = nodes[0].SelectSingleNode(nodes[0].XPath + "/em");
                        if (eraNode != null)
                        {
                            HtmlNode eraNameNode = eraNode.SelectSingleNode(eraNode.XPath + "/a");
                            if (eraNameNode != null)
                            {
                                era = eraNameNode.InnerText.Trim();
                            }
                        }

                    }
                }

                HtmlDocument docExt = ParserUtil.GetWeb(link + "/details.html");
                HtmlNode coverNode = docExt.DocumentNode.SelectSingleNode("//*[@id=\"movie_main\"]/div[1]/div/div[1]/a/img");
                if (coverNode != null)
                {
                    cover = coverNode.Attributes["src"].Value.Trim();
                }

                HtmlNodeCollection detailNodes =
                    docExt.DocumentNode.SelectNodes("//*[@id='movie_main_l']/div[2]/div/div");
                if (detailNodes != null)
                {
                    foreach (HtmlNode detailNode in detailNodes)
                    {
                        HtmlNode detailH3Nodes = docExt.DocumentNode.SelectSingleNode(detailNode.XPath + "/h3");
                        if (detailH3Nodes != null)
                        {
                            switch (detailH3Nodes.InnerText.Trim())
                            {
                                case "更多中文片名：":
                                    HtmlNodeCollection detailObjNodes = docExt.DocumentNode.SelectNodes(detailNode.XPath + "/ul/li");
                                    foreach (HtmlNode htmlNode in detailObjNodes)
                                    {
                                        string n = htmlNode.InnerText.Trim();
                                        if (!string.IsNullOrEmpty(n) && !othorNames.Contains(n))
                                        {
                                            othorNames.Add(n);
                                        }
                                    }
                                    break;
                                case "导演：":
                                    HtmlNodeCollection directObjNodes = docExt.DocumentNode.SelectNodes(detailNode.XPath + "/ul/li");
                                    foreach (HtmlNode htmlNode in directObjNodes)
                                    {
                                        HtmlNodeCollection directNameObjNodes = docExt.DocumentNode.SelectNodes(htmlNode.XPath + "/a");
                                        foreach (HtmlNode directNameObjNode in directNameObjNodes)
                                        {
                                            string n = directNameObjNode.InnerHtml.Trim();
                                            if (!string.IsNullOrEmpty(n))
                                            {
                                                director = new Character();
                                                string[] resultStrings = Regex.Split(n, "&nbsp;",
                                                                                    RegexOptions.IgnoreCase);
                                                if (resultStrings.Count() > 0)
                                                {
                                                    director.Name1 = resultStrings[0].Trim();
                                                    if (resultStrings.Count() > 1)
                                                    {
                                                        director.Name2 = resultStrings[1].Trim();
                                                    }
                                                }
                                            }
                                            break;
                                        }

                                    }
                                    break;
                                case "主演：":
                                    HtmlNodeCollection actorObjNodes = docExt.DocumentNode.SelectNodes(detailNode.XPath + "/ul/li");
                                    foreach (HtmlNode htmlNode in actorObjNodes)
                                    {
                                        HtmlNodeCollection directNameObjNodes = docExt.DocumentNode.SelectNodes(htmlNode.XPath + "/a");
                                        foreach (HtmlNode directNameObjNode in directNameObjNodes)
                                        {
                                            string n = directNameObjNode.InnerHtml.Trim();
                                            if (!string.IsNullOrEmpty(n) && !n.Contains("更多"))
                                            {
                                                Character actor = new Character();
                                                string[] resultStrings = Regex.Split(n, "&nbsp;",
                                                                                    RegexOptions.IgnoreCase);
                                                if (resultStrings.Count() > 0)
                                                {
                                                    actor.Name1 = resultStrings[0].Trim();
                                                    if (resultStrings.Count() > 1)
                                                    {
                                                        actor.Name2 = resultStrings[1].Trim();
                                                    }
                                                }
                                                actors.Add(actor);
                                            }
                                        }

                                    }
                                    break;

                                case "影片类型：":
                                    HtmlNodeCollection typeNodes = docExt.DocumentNode.SelectNodes(detailNode.XPath + "/ul/li/a");
                                    foreach (HtmlNode htmlNode in typeNodes)
                                    {
                                        types.Add(new Model.Type() { Name = htmlNode.InnerText.Trim() });
                                    }
                                    break;
                                case "国家/地区：":
                                    HtmlNodeCollection contryNodes = docExt.DocumentNode.SelectNodes(detailNode.XPath + "/ul/li/a");
                                    foreach (HtmlNode htmlNode in contryNodes)
                                    {
                                        countrys.Add(new Country() { Name = htmlNode.InnerText.Trim() });
                                    }
                                    break;
                                case "对白语言：":
                                    HtmlNodeCollection languageNodes = docExt.DocumentNode.SelectNodes(detailNode.XPath + "/ul/li/a");
                                    foreach (HtmlNode htmlNode in languageNodes)
                                    {
                                        languagea.Add(new Language() { Name = htmlNode.InnerText.Trim() });
                                    }
                                    break;
                            }
                        }

                    }
                }

                if (!string.IsNullOrEmpty(name))
                {
                    movie = new MovieInfo();
                    movie.Name = name;
                    movie.MainPic = cover;
                    //movie.Summary = sumary;
                    movie.OtherNames = othorNames;
                    movie.Director = director;
                    movie.Actors = actors;
                    movie.Types = types;
                    movie.Countrys = countrys;
                    movie.Languages = languagea;
                    movie.Era = era;
                    //movie.DownloadLinks = downLinks;
                    //movie.Source = link;
                    //movie.SourceName = "电影天堂";
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
