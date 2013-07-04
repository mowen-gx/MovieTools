using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.HtmlParser.MTime
{
    public class MovieInfoPageParser : IPageLinkParse
    {
        public void GetLinks(string link)
        {
            try
            {
                string pageStr = ParserUtil.HttpGet(link, "", "utf-8");
                ParserMsg.SetMsg("读取分页链接：" + link);
                if (!string.IsNullOrEmpty(pageStr))
                {
                    if (pageStr.IndexOf("=") > 0)
                    {
                        pageStr = pageStr.Substring(pageStr.IndexOf("=") + 2);
                        if (pageStr.IndexOf("}") > 0)
                        {
                            pageStr = pageStr.Substring(0, pageStr.LastIndexOf("}") + 1);
                            MtimePage page = null;
                            try
                            {
                                page = JsonDeserialize<MtimePage>(pageStr);
                            }
                            catch (Exception ex)
                            {
                                ParserMsg.SetMsg("JSON反序列化错误:" + ex.Message + ex.StackTrace);
                            }
                           
                            if (page != null)
                            {
                                if (page.value != null)
                                {
                                    HtmlDocument doc = new HtmlDocument();
                                    page.value.listHTML = page.value.listHTML.Replace("\\t", "");
                                    doc.LoadHtml( page.value.listHTML);
                                    ParseMovieLinkInPage(doc);
                                    if (page.value.totalCount > 0)
                                    {
                                        int totalPage = page.value.totalCount / 20 + (page.value.totalCount%20>0?1:0);
                                        for (int i = 2; i < totalPage; i++)
                                        {
                                            string t = DateTime.Now.Year + DateTime.Now.Month.ToString().Trim('0').Trim('/') +
                                                       DateTime.Now.Day.ToString().Trim('0').Trim('/') +
                                                       DateTime.Now.Hour.ToString().Trim('0').Trim('/') +
                                                       DateTime.Now.Minute.ToString().Trim('0').Trim('/') +
                                                       DateTime.Now.Second.ToString().Trim('0').Trim('/');

                                            int l = 18 - t.Length;
                                            for (int j = 0; j < l; j++)
                                            {
                                                t += (j + 1).ToString();
                                            }
                                            System.Threading.Thread.Sleep(15000);
                                            string pageLink =
                                                string.Format(link.Replace("Ajax_CallBackArgument18=1", "Ajax_CallBackArgument18={0}"), i);
                                            pageLink = BuildUrl(pageLink,"t",t);
                                            ParsePageLink(pageLink);
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

        public static string BuildUrl(string url, string paramText, string paramValue)
        {
            Regex reg = new Regex(string.Format("{0}=[^&]*", paramText), RegexOptions.IgnoreCase);
            Regex reg1 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
            string urlTemp= reg.Replace(url, "");
            //_url = reg1.Replace(_url, "");
            if (urlTemp.IndexOf("?") == -1)
                urlTemp += string.Format("?{0}={1}", paramText, paramValue);//?
            else
                urlTemp += string.Format("&{0}={1}", paramText, paramValue);//&
            urlTemp = reg1.Replace(urlTemp, "&");
            urlTemp = urlTemp.Replace("?&", "?");
            return urlTemp;
        }

        private void ParsePageLink(string link)
        {
            try
            {
                string pageStr = ParserUtil.HttpGet(link, "", "utf-8");
                ParserMsg.SetMsg("读取分页链接：" + link);
                if (!string.IsNullOrEmpty(pageStr))
                {
                    if (pageStr.IndexOf("=") > 0)
                    {
                        pageStr = pageStr.Substring(pageStr.IndexOf("=") + 2);
                        if (pageStr.IndexOf("}") > 0)
                        {
                            pageStr = pageStr.Substring(0, pageStr.LastIndexOf("}") + 1);
                            MtimePage page = null;
                            try
                            {
                                page = JsonDeserialize<MtimePage>(pageStr);
                            }
                            catch (Exception ex)
                            {
                                ParserMsg.SetMsg("JSON反序列化错误:" + ex.Message + ex.StackTrace);
                            }
                            if (page != null)
                            {
                                if (page.value != null)
                                {
                                    HtmlDocument doc = new HtmlDocument();
                                    page.value.listHTML = page.value.listHTML.Replace("\\t", "");
                                    doc.LoadHtml(page.value.listHTML);
                                    ParseMovieLinkInPage(doc);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ParserMsg.SetMsg("解析分页链接错误:" + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        private void ParseMovieLinkInPage(HtmlDocument doc)
        {
            try
            {
               List<string> movielinks = new List<string>();
                HtmlNodeCollection licollection = doc.DocumentNode.SelectNodes("/ul/li");
                if (licollection != null)
                {
                    foreach (HtmlNode htmlNode in licollection)
                    {
                        HtmlNodeCollection tablelicollection = htmlNode.SelectNodes(htmlNode.XPath + "/div");
                        if (tablelicollection != null)
                        {
                            foreach (HtmlNode node in tablelicollection)
                            {
                                HtmlNode aNode = node.SelectSingleNode(node.XPath + "/div/div[2]/h3/a");
                                if (aNode != null)
                                {
                                    string name = aNode.InnerText.Trim();
                                    string link = aNode.Attributes["href"].Value;
                                    if (!String.IsNullOrEmpty(link) && !movielinks.Contains(link))
                                    {
                                        movielinks.Add(link);
                                    }

                                    HtmlNodeCollection starNodes = node.SelectNodes(node.XPath + "/div/div[2]/div/p/span");
                                    if (starNodes != null)
                                    {
                                        int star = 0;
                                        if (starNodes.Count > 0)
                                        {
                                            int.TryParse(starNodes[0].InnerText.Trim(), out star);
                                        }
                                        int minStar = 0;
                                        if (starNodes.Count > 1)
                                        {
                                            int.TryParse(starNodes[1].InnerText.Trim(), out minStar);
                                        }
                                        star = star * 10 + minStar;
                                        Data.SetMovieStar(name, star);
                                    }
                                }
                            }
                        }
                    }
                    Service.Data.SetMovieInfoLinks(movielinks);
                }
            }
            catch (Exception ex)
            {
                ParserMsg.SetMsg("错误:" + ex.Message + ex.StackTrace);
            }
        }
    }
}
