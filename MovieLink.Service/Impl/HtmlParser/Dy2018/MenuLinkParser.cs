using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.HtmlParser.Dy2018
{
    public class MenuLinkParser : IMenuLinkParse
    {
        public List<string> GetLinks(string url)
        {
            List<string> links = new List<string>();
            HtmlDocument doc = ParserUtil.GetWeb(url);
            HtmlNode node = doc.GetElementbyId("menu");
            if (node != null)
            {
                HtmlNodeCollection collection = node.SelectNodes(node.XPath + "//div/ul/li/a");
                if (collection != null)
                {
                    foreach (HtmlNode htmlNode in collection)
                    {
                        string link = "http://www.dy2018.com" + htmlNode.Attributes["href"].Value;
                        string typeName = htmlNode.InnerText.Trim().Trim('片');
                        if (!String.IsNullOrEmpty(typeName)
                            && typeName != "收藏本站"
                            && typeName != "加入本站"
                            && typeName != "时尚女性"
                            && typeName != "设为主页"
                            && typeName != "快车电影"
                            && typeName != "影视交流"
                            && typeName != "游戏下载"
                            && !String.IsNullOrEmpty(link) && !links.Contains(link))
                        {
                            links.Add(link);
                        }
                    }
                }
            }
            return links;
        }
    }
}
