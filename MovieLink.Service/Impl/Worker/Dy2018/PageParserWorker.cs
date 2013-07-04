using MovieLink.Service.Impl.HtmlParser.Dy2018;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.Worker.Dy2018
{
    public class PageParserWorker : BasePageParserWorker
    {
        private readonly IMenuLinkParse _menuLinkParser;
        private readonly IPageLinkParse _pageLinkParser;
        public PageParserWorker()
        {
            this._menuLinkParser = new MenuLinkParser();
            this._pageLinkParser = new PageLinkParser();
        }

        public override IPageLinkParse GetPageLinkParser()
        {
            return _pageLinkParser;
        }

        public override IMenuLinkParse GetMenuLinkParser()
        {
            return _menuLinkParser;
        }

        public override string GetUrl()
        {
            return "http://www.dy2018.com/";
        }
    }
}
