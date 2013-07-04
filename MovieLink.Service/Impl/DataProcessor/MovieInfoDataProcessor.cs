using System;
using System.Collections.Generic;
using System.Linq;
using MovieLink.Data.MsSql;
using MovieLink.Model;
using MovieLink.Service.Interface.DataProcessor;

namespace MovieLink.Service.Impl.DataProcessor
{
    public class MovieInfoDataProcessor : IInsertDataProcess
    {
        public void Run()
        {
            MovieData movieData = new MovieData();
            MovieNamesData movieOtherNameData = new MovieNamesData();
            Dictionary<string, Model.Type> types = movieData.GetTypes();
            Dictionary<string, Country> countries = movieData.GetCountries();
            Dictionary<string, Language> languages = movieData.GetLanguages();
            while (!Data.IsDoneMovieFinish && !Data.IsCancel)
            {
                try
                {
                    List<Movie> movies = Data.GetMovies(10);
                    foreach (Movie movie in movies)
                    {
                        try
                        {
                            MovieInfo movieInfo = (MovieInfo)movie;
                            if (movieInfo == null)
                            {
                                continue;
                            }
                            DbMsg.SetMsg("添加电影:《" + movieInfo.Name + "》");
                            Movie movieFromDb = movieData.GetGuidByName(movieInfo.Name.Trim());
                            if (movieFromDb == null)
                            {
                                movieInfo.Guid = Guid.NewGuid().ToString();
                                DbMsg.SetMsg("将电影 《" + movieInfo.Name + "》信息存入数据库");
                                movieData.Add(movieInfo);
                            }
                            else
                            {
                                movieInfo.Guid = movieFromDb.Guid;
                                DbMsg.SetMsg("电影:《" + movieInfo.Name + "》已存在");
                                if (movieFromDb.IsSyn == 0)
                                {
                                    DbMsg.SetMsg("更新电影:《" + movieInfo.Name + "》");
                                    movieData.Update(movieInfo);
                                }
                            }

                            MovieName movieName = new MovieName()
                            {
                                MovieGuid = movieInfo.Guid,
                                Name = movieInfo.Name
                            };

                            if (movieInfo.OtherNames.Count > 0)
                            {
                                foreach (string name in movieInfo.OtherNames)
                                {
                                    var otherName = new MovieName()
                                    {
                                        MovieGuid = movieInfo.Guid,
                                        Name = name
                                    };

                                    DbMsg.SetMsg("查询电影 《" + movieInfo.Name + "》别名信息:" + name);
                                    if (!movieOtherNameData.IsExist(otherName))
                                    {
                                        DbMsg.SetMsg("将电影 《" + movieInfo.Name + "》别名:" + name + "存入数据库");
                                        movieOtherNameData.AddMovieOtherName(otherName);
                                    }
                                    else
                                    {
                                        DbMsg.SetMsg("电影《" + movieInfo.Name + "》别名" + name + "已存在");
                                    }
                                }
                            }

                            List<Country> countriesOfMovie = new List<Country>();
                            foreach (Country country in movieInfo.Countrys)
                            {
                                if (countries.ContainsKey(country.Name))
                                {
                                    countriesOfMovie.Add(countries[country.Name]);
                                }
                            }

                            List<Model.Type> typeOfMovie = new List<Model.Type>();
                            foreach (Model.Type type in movieInfo.Types)
                            {
                                if (types.ContainsKey(type.Name))
                                {
                                    typeOfMovie.Add(types[type.Name]);
                                }
                            }

                            List<Model.Language> languageOfMovie = new List<Model.Language>();
                            foreach (Model.Language language in movieInfo.Languages)
                            {
                                if (languages.ContainsKey(language.Name))
                                {
                                    languageOfMovie.Add(languages[language.Name]);
                                }
                            }

                            movieData.AddMovieCountries(movieInfo.Guid, countriesOfMovie);
                            movieData.AddMovieLanguages(movieInfo.Guid, languageOfMovie);
                            movieData.AddMovieTypes(movieInfo.Guid, typeOfMovie);
                        }
                        catch (Exception ex)
                        {
                            DbMsg.SetMsg("添加电影出错:" + ex.Message + ex.StackTrace);
                        }
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
