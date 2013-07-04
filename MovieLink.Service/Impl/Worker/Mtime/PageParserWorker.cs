using MovieLink.Service.Impl.HtmlParser.MTime;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.Worker.Mtime
{
    public class PageParserWorker : BasePageParserWorker
    {
        private readonly IMenuLinkParse _menuLinkParser;
        private readonly IPageLinkParse _pageLinkParser;
        public PageParserWorker()
        {
            this._menuLinkParser = new MovieInfoMenuParser();
            this._pageLinkParser = new MovieInfoPageParser();
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
            return "http://movie.mtime.com/movie/search/section/";
        }
    }
}
