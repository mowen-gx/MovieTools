using MovieLink.Service.Impl.HtmlParser.Dy2018;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.Worker.Dy2018
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
            return Util.MovieType.Dy2018;
        }
    }
}
