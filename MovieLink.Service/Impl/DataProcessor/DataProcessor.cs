using System;
using System.Collections.Generic;
using System.Linq;
using MovieLink.Data.MsSql;
using MovieLink.Model;
using MovieLink.Service.Interface.DataProcessor;

namespace MovieLink.Service.Impl.DataProcessor
{
    public class DataProcessor : IInsertDataProcess
    {
        public void Run()
        {
            MovieData movieData = new MovieData();
            MovieNamesData movieOtherNameData = new MovieNamesData();
            var downloadLinkData = new DownloadLinkData();
            var movieSummaryData = new MovieSummaryData();
            while (!Data.IsDoneMovieFinish && !Data.IsCancel)
            {
                try
                {
                    List<Movie> movies = Data.GetMovies(10);
                    foreach (Movie movie in movies)
                    {
                        DbMsg.SetMsg("添加电影:《" + movie.Name+"》");
                        Movie movieFromDb = movieData.GetGuidByName(movie.Name.Trim());
                        if (movieFromDb == null)
                        {
                            movie.Guid = Guid.NewGuid().ToString();
                            DbMsg.SetMsg("将电影 《" + movie.Name + "》信息存入数据库");
                            movieData.Add(movie);
                        }
                        else
                        {
                            DbMsg.SetMsg("电影:《" + movie.Name + "》已存在");
                            movie.Guid = movieFromDb.Guid;
                        }

                        MovieName movieName = new MovieName()
                        {
                            MovieGuid = movie.Guid,
                            Name = movie.Name
                        };

                        if (!movieOtherNameData.IsExist(movieName))
                        {
                            DbMsg.SetMsg("将电影 《" + movie.Name + "》别名存入数据库");
                            movieOtherNameData.AddMovieOtherName(movieName);
                        }

                        if (movie.OtherNames.Count > 0)
                        {
                            foreach (string name in movie.OtherNames)
                            {
                                var otherName = new MovieName()
                                {
                                    MovieGuid = movie.Guid,
                                    Name = name
                                };

                                DbMsg.SetMsg("查询电影 《" + movie.Name + "》别名信息:" + name);
                                if (!movieOtherNameData.IsExist(otherName))
                                {
                                    DbMsg.SetMsg("将电影 《" + movie.Name + "》别名:" + name + "存入数据库");
                                    movieOtherNameData.AddMovieOtherName(otherName);
                                }
                                else
                                {
                                    DbMsg.SetMsg("电影《" + movie.Name + "》别名" + name + "已存在");
                                }
                            }
                        }

                        if (movie.DownloadLinks.Count > 0)
                        {
                            foreach (string downloadLink in movie.DownloadLinks)
                            {
                                if (!downloadLinkData.IsExist(downloadLink))
                                {
                                    DbMsg.SetMsg("插入电影 《" + movie.Name + "》链接到数据库:" + downloadLink);
                                    downloadLinkData.Add(new DownloadLink()
                                    {
                                        BusinessGuid = movie.Guid,
                                        Guid = Guid.NewGuid().ToString(),
                                        LinkAddr = downloadLink,
                                        Source = movie.Source,
                                        SourceName = "飘花"
                                    });
                                }
                                else
                                {
                                    DbMsg.SetMsg("电影 《" + movie.Name + "》链接已存在");
                                }
                            }
                        }
                        movieSummaryData.AddSummary(movie.Guid,movie.Summary);
                    }

                    List<string> links = movies.Select(movie => movie.Source).ToList();
                    Data.SetDoneMovies(links);
                }
                catch (Exception ex)
                {
                    DbMsg.SetMsg("添加电影出错:" + ex.Message + ex.StackTrace);
                }
            }
            if (Data.IsCancel)
            {
                DbMsg.SetMsg("任务取消完成");
                ParserMsg.SetMsg("任务取消完成");
                Data.IsCanceled = true;
            }
            DbMsg.SetMsg("添加电影完成");
        }
    }
}
