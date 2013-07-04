using MovieLink.Service.Impl.HtmlParser.Piaohua;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.Worker.Piaohua
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
            return "http://www.piaohua.com/";
        }
    }
}
