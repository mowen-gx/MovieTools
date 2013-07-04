using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.HtmlParser.Piaohua
{
    public class MenuLinkParser : IMenuLinkParse
    {
        public List<string> GetLinks(string url)
        {
            List<string> links = new List<string>();
            HtmlWeb webClient = new HtmlWeb();
            HtmlDocument doc = webClient.Load(url);
            HtmlNode node = doc.GetElementbyId("menu");
            if (node != null)
            {
                HtmlNodeCollection collection = node.SelectNodes(node.XPath + "//ul/li/a");
                if (collection != null)
                {
                    foreach (HtmlNode htmlNode in collection)
                    {
                        string link = "http://www.piaohua.com" + htmlNode.Attributes["href"].Value;
                        string typeName = htmlNode.InnerText.Trim().Trim('片');
                        if (!String.IsNullOrEmpty(typeName) && typeName != "飘花首页" && typeName != "今日更新的电影" &&
                            !String.IsNullOrEmpty(link) && !links.Contains(link) )
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
