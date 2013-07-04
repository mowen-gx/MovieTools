using System;
using System.Collections.Generic;
using MovieLink.Model;
using MovieLink.Service.Interface.HtmlParser;
using MovieLink.Service.Interface.Worker;

namespace MovieLink.Service.Impl.Worker
{
    public abstract class BaseMovieParseWorker : IMovieParseWork
    {
        public abstract IDetaiParse GetDetaiParser();
        public abstract string GetMovieType();

        public void Run()
        {
            Data.IsGetMovieFinish = false;
            while (!Data.IsDoneDetailLinkFinish(GetMovieType()) && !Data.IsCancel)
            {
                try
                {
                    List<string> detailLinks = Data.GetDetailLinks(GetMovieType(),100);
                    foreach (string link in detailLinks)
                    {
                        ParserMsg.SetMsg("读取电影:" + link);
                        Movie movie = GetDetaiParser().GetDetail(link);
                        if (movie != null)
                        {
                            ParserMsg.SetMsg("读取到电影名为:" + (movie.Name??string.Empty));
                            Data.SetMovies(new List<Movie>(){movie});
                        }
                        Data.SetDoneDetailLink(new List<string>(){link});
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
