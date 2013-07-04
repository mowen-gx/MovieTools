using System;
using System.Collections.Generic;
using MovieLink.Model;
using MovieLink.Service.Impl.HtmlParser.MTime;
using MovieLink.Service.Interface.HtmlParser;

namespace MovieLink.Service.Impl.Worker.Mtime
{
    class MovieParseWorker : BaseMovieParseWorker
    {
        IDetaiParse _detaiParser = new MovieInfoDetailParser();

        public override IDetaiParse GetDetaiParser()
        {
            return _detaiParser;
        }

        public override string GetMovieType()
        {
            return Util.MovieType.Mtime;
        }

        public void Run()
        {
            Data.IsGetMovieFinish = false;
            while (!Data.IsDoneDetailLinkFinish(GetMovieType()) && !Data.IsCancel)
            {
                try
                {
                    List<string> detailLinks = Data.GetMovieInfoLinks(100);
                    foreach (string link in detailLinks)
                    {
                        ParserMsg.SetMsg("读取电影:" + link);
                        System.Threading.Thread.Sleep(1000);
                        Movie movie = GetDetaiParser().GetDetail(link);
                        if (movie != null)
                        {
                            ParserMsg.SetMsg("读取到电影名为:" + (movie.Name ?? string.Empty));
                            Data.SetMovies(new List<Movie>() { movie });
                        }
                        Data.SetDoneMovieInfoLinks(new List<string>() { link });
                    }
                }
                catch (Exception ex)
                {
                    ParserMsg.SetMsg("BaseMovieParseWorker.Run()发生错误：" + ex.Message + ex.StackTrace);
                }
            }
            ParserMsg.SetMsg("读取电影链接完成");
            Data.IsGetMovieFinish = true;
        } 
    }
}
