using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MovieLink.Service.Impl.HtmlParser
{
    public class ParserUtil
    {
        public static HtmlDocument GetWeb(string url)
        {
            HtmlDocument doc = null;
            HttpWebRequest req;
            req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.Method = "GET";
            WebResponse rs = req.GetResponse();
            Stream rss = rs.GetResponseStream();
            try
            {
                doc = new HtmlDocument();
                doc.Load(rss);
            }
            catch (Exception e)
            {
            }
            return doc;
        }

        public static string HttpGet(string url, string postDataStr,string encode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=" + encode.ToUpper();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encode));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public static List<HtmlNode> GetNodesByClass(HtmlNode node,string className )
        {
            if (node != null)
            {
                List<HtmlNode> rets = new List<HtmlNode>();
                HtmlNodeCollection nodes = node.SelectNodes("//div[@class='" + className.Trim() + "']");
                if (nodes != null)
                {
                    rets = nodes.ToList();
                }
                return rets;
            }
            return new List<HtmlNode>();
        }

        public static List<HtmlNode> GetNodesByClass(HtmlNode node, string className,string tagName)
        {
            if (node != null)
            {
                List<HtmlNode> rets = new List<HtmlNode>();
                HtmlNodeCollection nodes = node.SelectNodes("//" + tagName.Trim() + "[@class='" + className.Trim() + "']");
                if (nodes != null)
                {
                    rets = nodes.ToList();
                }
                return rets;
            }
            return new List<HtmlNode>();
        }

        public static List<HtmlNode> GetNodesByName(HtmlNode node, string name)
        {
            if (node != null)
            {
                return node.SelectNodes("//div[@name=\"" + name.Trim() + "\"]").ToList();
            }
            return new List<HtmlNode>();
        }
    }
}
