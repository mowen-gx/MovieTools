using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.HtmlParser.Dy2018
{
    public class PageLinkParser : IPageLinkParse
    {
        public void GetLinks(string link)
        {
            try
            {
                HtmlDocument doc = ParserUtil.GetWeb(link);
                ParserMsg.SetMsg("读取分页链接：" + link);
                string pageTemp = "";
                int total = GetTotalPage(doc, ref pageTemp);
                ParserMsg.SetMsg("读取分页链接：" + link + "，共有" + total + "页，开始读取第1页");
                var links = new List<string>();
                ParseMovieLinkInPage(doc, ref links);
                Data.SetDetailLink(Util.MovieType.Dy2018,links);
                if (total > 1)
                {
                    for (int i = 2; i <= total; i++)
                    {
                        ParserMsg.SetMsg("读取分页链接：" + link + "，共有" + total + "页，开始读取第" + i + "页");
                        try
                        {
                            string linkTemp = link.Replace("index.html", pageTemp);
                            HtmlDocument docList = ParserUtil.GetWeb(string.Format(linkTemp, i));
                            links = new List<string>();
                            ParseMovieLinkInPage(docList, ref links);
                            Data.SetDetailLink(Util.MovieType.Dy2018, links);
                        }
                        catch (Exception ex)
                        {
                            ParserMsg.SetMsg("错误:" + ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ParserMsg.SetMsg("错误:" + ex.Message + ex.StackTrace);
            }
        }

        private void ParseMovieLinkInPage(HtmlDocument doc, ref List<string> movielinks)
        {
            try
            {
                List<HtmlNode> nodes = ParserUtil.GetNodesByClass(doc.DocumentNode, "co_content8");
                if (nodes != null)
                {
                    foreach (HtmlNode htmlNode in nodes)
                    {
                        HtmlNodeCollection collection = htmlNode.SelectNodes(htmlNode.XPath + "/ul/td/table");
                        if (collection != null)
                        {
                            foreach (HtmlNode node in collection)
                            {
                                HtmlNodeCollection trCollection = node.SelectNodes(node.XPath + "/tr");
                                if (trCollection != null)
                                {
                                    foreach (HtmlNode trNode in trCollection)
                                    {
                                        HtmlNodeCollection tdCollection = trNode.SelectNodes(trNode.XPath + "/td");
                                        if (tdCollection != null)
                                        {
                                            foreach (HtmlNode tdNode in tdCollection)
                                            {
                                                HtmlNodeCollection aCollection = tdNode.SelectNodes(tdNode.XPath + "/b/a[1]");
                                                if (aCollection != null)
                                                {
                                                    foreach (HtmlNode aNode in aCollection)
                                                    {
                                                        string link = "http://www.dy2018.com" + aNode.Attributes["href"].Value;
                                                        if (!String.IsNullOrEmpty(link) && !movielinks.Contains(link))
                                                        {
                                                            movielinks.Add(link);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ParserMsg.SetMsg("错误:" + ex.Message + ex.StackTrace);
            }
        }

        private int GetTotalPage(HtmlDocument doc, ref string pageTemp)
        {
            int total = 1;
            try
            {
                bool isFind = false;
                List<HtmlNode> nodes = ParserUtil.GetNodesByClass(doc.DocumentNode, "x");
                if (nodes.Count > 0)
                {
                    foreach (HtmlNode htmlNode in nodes)
                    {
                        HtmlNodeCollection nodeCollection = htmlNode.SelectNodes(htmlNode.XPath + "/td/a");
                        if (nodeCollection != null)
                        {
                            foreach (HtmlNode node in nodeCollection)
                            {
                                if (node.InnerText.Trim() == "末页")
                                {
                                    if (node.Attributes["href"] != null)
                                    {
                                        string link = node.Attributes["href"].Value.ToLower();
                                        pageTemp = "list_";
                                        link = link.Replace("list_", "");
                                        if (link.IndexOf("_") >= 0 && (link.Length > (link.IndexOf("_") + 2)))
                                        {
                                            pageTemp += link.Substring(0, link.IndexOf("_")) + "_{0}" + ".html";
                                            link = link.Substring(link.IndexOf("_") + 1);
                                            int.TryParse(link.Substring(0, link.IndexOf(".")), out total);
                                        }
                                    }
                                    isFind = true;
                                    break;
                                }
                            }
                        }

                        if (isFind)
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ParserMsg.SetMsg("错误:" + ex.Message + ex.StackTrace);
            }
            return total;
        }
    }
}
