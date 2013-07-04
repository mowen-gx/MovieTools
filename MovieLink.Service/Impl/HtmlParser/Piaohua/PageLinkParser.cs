using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.HtmlParser.Piaohua
{
    public class PageLinkParser : IPageLinkParse
    {
        public void GetLinks(string link)
        {
            try
            {
                HtmlWeb webClient = new HtmlWeb();
                HtmlDocument doc = webClient.Load(link);
                ParserMsg.SetMsg("读取分页链接：" + link);
                int total = GetTotalPage(doc);
                ParserMsg.SetMsg("读取分页链接：" + link + "，共有" + total + "页，开始读取第1页");
                var links = new List<string>(); 
                ParseMovieLinkInPage(doc, ref links);
                Data.SetDetailLink(Util.MovieType.Piaohua, links);
                if (total > 1)
                {
                    for (int i = 2; i <= total; i++)
                    {
                        ParserMsg.SetMsg("读取分页链接：" + link + "，共有" + total + "页，开始读取第" + i + "页");
                        try
                        {
                            string linkTemp = link.Replace("/index.html", "/list_{0}.html");
                            HtmlWeb webClientList = new HtmlWeb();
                            HtmlDocument docList =
                                webClientList.Load(string.Format(linkTemp, i));
                            links = new List<string>();
                            ParseMovieLinkInPage(docList, ref links);
                            Data.SetDetailLink(Util.MovieType.Piaohua, links);
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
                HtmlNode node = doc.GetElementbyId("list");
                if (node != null)
                {
                    HtmlNodeCollection collection = node.SelectNodes(node.XPath + "//dl/dt/a");
                    if (collection != null)
                    {
                        foreach (HtmlNode htmlNode in collection)
                        {
                            string link = "http://www.piaohua.com" + htmlNode.Attributes["href"].Value;
                            if (!String.IsNullOrEmpty(link) && !movielinks.Contains(link))
                            {
                                movielinks.Add(link);
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

        private int GetTotalPage(HtmlDocument doc)
        {
            int total = 1;
            try
            {
                HtmlNode node = doc.GetElementbyId("nml");
                if (node != null)
                {
                    HtmlNodeCollection collection = node.SelectNodes(node.XPath + "//div[2]/a");
                    if (collection != null)
                    {
                        foreach (HtmlNode htmlNode in collection)
                        {
                            string text = htmlNode.InnerText.Trim();
                            if (text == "末页")
                            {
                                string link = htmlNode.Attributes["href"].Value;
                                string pageStr = link.Substring(link.IndexOf("list_") + 5, (link.LastIndexOf(".") - (link.IndexOf("list_") + 5)));
                                int.TryParse(pageStr, out total);
                            }
                        }
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
