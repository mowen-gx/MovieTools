using MovieLink.Model;

namespace MovieLink.Data
{
    public interface IMovieOtherNameData
    {
        bool AddMovieOtherName(MovieName movieName);

        /// <summary>
        ///电影别名信息是否存在
        /// </summary>
        /// <param name="movieName">电影别名信息</param>
        /// <returns></returns>
        bool IsExist(MovieName movieName);

        /// <summary>
        ///根据名字获取电影信息
        /// </summary>
        /// <param name="name">电影名字</param>
        /// <returns></returns>
        MovieName GetByName(string name);
    }
}