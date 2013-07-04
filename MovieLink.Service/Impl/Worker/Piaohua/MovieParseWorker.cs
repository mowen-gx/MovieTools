using MovieLink.Service.Impl.HtmlParser.Piaohua;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.Worker.Piaohua
{
    public class MovieParseWorker : BaseMovieParseWorker
    {
        IDetaiParse _detaiParser = new DetailParser();

        public override IDetaiParse GetDetaiParser()
        {
            return _detaiParser;
        }

        public override string GetMovieType()
        {
            return Util.MovieType.Piaohua;
        }
    }
}
