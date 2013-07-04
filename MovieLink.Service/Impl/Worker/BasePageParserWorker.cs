using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLink.Service.Interface.HtmlParser;
using MovieLink.Service.Interface.Worker;

namespace MovieLink.Service.Impl.Worker
{
    public abstract class BasePageParserWorker : IPageParseWork
    {
        public abstract IPageLinkParse GetPageLinkParser();
        public abstract IMenuLinkParse GetMenuLinkParser();

        public abstract string GetUrl();

        public void Run()
        {
            List<string> links = GetMenuLinkParser().GetLinks(GetUrl());
            ParserMsg.SetMsg("读取分页链接开始");
            Data.IsGetDetailLinkFinish = false;
            foreach (string link in links)
            {
                if (!string.IsNullOrEmpty(link))
                {
                    System.Threading.Thread.Sleep(3000);
                    GetPageLinkParser().GetLinks(link);
                }
            }

            Data.IsGetDetailLinkFinish = true;
            ParserMsg.SetMsg("读取分页链接完成");
        }
    }
}
