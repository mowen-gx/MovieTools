using System.Collections;
using System.Collections.Generic;
using MovieLink.Data.MsSql;
using MovieLink.Model;

namespace MovieLink.Service
{
    public class Data
    {
        private static readonly object Obj = new object();
        private static LinksData _data = new LinksData();
        /// <summary>
        /// 是否取消 
        /// </summary>
        public static bool IsCancel { get; set; }

        /// <summary>
        /// 是否已经取消完成 
        /// </summary>
        public static bool IsCanceled { get; set; }

      
        private static int _hasDoneDetailCount;
        private static List<string> _detailLinks = new List<string>();
        private static bool _isGetDetailLinkFinish;
        public static bool IsGetDetailLinkFinish
        {
            get
            {
                return _isGetDetailLinkFinish;
            }
            set
            {
                _isGetDetailLinkFinish = value;
            }
        }

        public static bool IsDoneDetailLinkFinish(string type)
        {
            return IsGetDetailLinkFinish && (_data.GetNotParseLinkCount(type) == 0);
        }

        public static int HasGetWebDetailLinkCount()
        {
            return _detailLinks.Count;
        }

        public static List<string> GetDetailLinks(string type, int count)
        {
            lock (Obj)
            {
                return _data.GetNotParseLink(type, count) ?? new List<string>();
            }
            //lock (Obj)
            //{
            //    //int allCount = _detailLinks.Count;
            //    //if (allCount > 0 && _hasGetDetailCount < allCount)
            //    //{
            //    //    int startIndex = _hasGetDetailCount;
            //    //    if (startIndex < 0)
            //    //    {
            //    //        startIndex = 0;
            //    //    }
            //    //    int endIndex = _hasGetDetailCount-1 + (endIndex-startIndex)+1;
            //    //    if (endIndex > (allCount - 1))
            //    //    {
            //    //        endIndex = allCount - 1;
            //    //    }
            //    //    _hasGetDetailCount = _hasGetDetailCount + count;

            //    //    for (int i = startIndex; i <= endIndex; i++)
            //    //    {
            //    //        ret.Add(_detailLinks[i]);
            //    //    }
            //    //}
            //}
        }

        public static void SetDetailLink(string type, List<string> links)
        {
            lock (Obj)
            {
                foreach (string link in links)
                {
                    if (!_detailLinks.Contains(link))
                    {
                        _detailLinks.Add(link);
                    }
                }
                //链接放入数据库
                _data.AddLinks(type, links);
            }
        }

        public static void SetDoneDetailLink(List<string> links)
        {
            lock (Obj)
            {
                _hasDoneDetailCount = _hasDoneDetailCount + links.Count;
            }
            _data.SetDoneParseLink(links);
        }

        public static int GetNotParseDetailLinkFromDb(string type)
        {
            return _data.GetNotParseLinkCount(type);
        }

        public static int GetDoneDetailLinkCount()
        {
            return _hasDoneDetailCount;
        }

        private static int _hasGetMovieCount = 0;
        private static int _hasDoneMovieCount = 0;
        private static List<Movie> _movies = new List<Movie>();

        private static bool _isGetMovieFinish;
        public static bool IsGetMovieFinish
        {
            get
            {
                return _isGetMovieFinish;
            }
            set
            {
                _isGetMovieFinish = value;
            }
        }
        public static bool IsDoneMovieFinish
        {
            get { return IsGetMovieFinish && (_movies.Count <= _hasDoneMovieCount); }
        }

        public static List<Movie> GetMovies(int count)
        {
            lock (Obj)
            {
                List<Movie> ret = new List<Movie>();
                int allCount = _movies.Count;
                if (allCount > 0 && _hasGetMovieCount < allCount)
                {
                    int startIndex = _hasGetMovieCount;
                    if (startIndex < 0)
                    {
                        startIndex = 0;
                    }
                    int endIndex = _hasGetMovieCount - 1 + count;
                    if (endIndex > (allCount - 1))
                    {
                        endIndex = allCount - 1;
                    }

                    _hasGetMovieCount = _hasGetMovieCount + (endIndex - startIndex) + 1;
                    for (int i = startIndex; i <= endIndex; i++)
                    {
                        ret.Add(_movies[i]);
                    }
                }
                return ret;
            }
        }
       
        public static void SetMovies(List<Movie> movies)
        {
            lock (Obj)
            {
                _movies.AddRange(movies);
            }
        }

        public static void SetDoneMovies(List<string> links)
        {
            lock (Obj)
            {
                if (links.Count > 0)
                {
                    _hasDoneMovieCount = _hasDoneMovieCount + links.Count;
                    _data.SetDoneAddToDbLink(links);
                }
            }
        }

        public static int GetDoneMovieCount()
        {
            return _hasDoneMovieCount;
        }

        private static int _hasGetMovieInfoDetailCount = 0;
        private static int _hasDoneMovieInfoCount = 0;
        private static List<string> _movieInfoDetailLinks = new List<string>();
        private static bool _isGetMovieInfoDetailLinkFinish;
        public static bool IsGetMovieInfoDetailLinkFinish
        {
            get
            {
                return _isGetMovieInfoDetailLinkFinish;
            }
            set
            {
                _isGetMovieInfoDetailLinkFinish = value;
            }
        }

        public static void SetMovieInfoLinks(List<string> links)
        {
            lock (Obj)
            {
                foreach (string link in links)
                {
                    if(!_movieInfoDetailLinks.Contains(link))
                        _movieInfoDetailLinks.Add(link);
                }
            }
        }

        public static void SetDoneMovieInfoLinks(List<string> links)
        {
            lock (Obj)
            {
                _hasDoneMovieInfoCount = _hasDoneMovieInfoCount + links.Count;
            }
        }

        public static int GetDoneMovieInfoLinks()
        {
            return _hasDoneMovieInfoCount;
        }

        public static int GetNotParseMovieInfoLinks()
        {
            lock (Obj)
            {
                return _movieInfoDetailLinks.Count - _hasDoneMovieInfoCount;
            }
        }

        public static int HasGetWebDetailInfoLinkCount()
        {
            return _movieInfoDetailLinks.Count;
        }

        public static List<string> GetMovieInfoLinks(int count)
        {
            lock (Obj)
            {
                List<string> ret = new List<string>();
                int allCount = _movieInfoDetailLinks.Count;
                if (allCount > 0 && _hasGetMovieInfoDetailCount < allCount)
                {
                    int startIndex = _hasGetMovieInfoDetailCount;
                    if (startIndex < 0)
                    {
                        startIndex = 0;
                    }
                    int endIndex = _hasGetMovieInfoDetailCount - 1 + count;
                    if (endIndex > (allCount - 1))
                    {
                        endIndex = allCount - 1;
                    }

                    _hasGetMovieInfoDetailCount = _hasGetMovieInfoDetailCount + (endIndex - startIndex) + 1;
                    for (int i = startIndex; i <= endIndex; i++)
                    {
                        ret.Add(_movieInfoDetailLinks[i]);
                    }
                }
                return ret;
            }
        }

        private static Hashtable _movieStarInfo = new Hashtable();
        public static void SetMovieStar(string movieName, int star)
        {
            lock (Obj)
            {
                if (_movieStarInfo.ContainsKey(movieName))
                {
                    _movieStarInfo[movieName] = star;
                }
                else
                {
                    _movieStarInfo.Add(movieName,star);
                }
            }
        }

        public static int GetMovieStar(string movieName)
        {
            lock (Obj)
            {
                int star = 0;
                if (_movieStarInfo.ContainsKey(movieName))
                {
                    int.TryParse(_movieStarInfo[movieName].ToString(),out star);
                }
                return star;
            }
        }

        /// <summary>
        /// 重设
        /// </summary>
        public static void Reset(string type)
        {
            lock (Obj)
            {
                _hasDoneDetailCount = 0;
                _hasGetMovieCount = 0;
                _hasDoneMovieCount = 0;
                _detailLinks = new List<string>();
                _movies = new List<Movie>();
                _isGetDetailLinkFinish = false;
                _isGetMovieFinish = false;
                IsCancel = false;
                IsCanceled = false;
                _movieInfoDetailLinks = new List<string>();
                _data.ResetNotParseLinks(type);
                _hasDoneMovieInfoCount = 0;
                _hasGetMovieInfoDetailCount = 0;
            }
        }
    }
}
