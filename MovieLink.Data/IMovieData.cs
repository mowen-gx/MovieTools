using MovieLink.Model;

namespace MovieLink.Data
{
    public interface IMovieData
    {
        /// <summary>
        /// 添加电影信息
        /// </summary>
        /// <param name="movie">电影信息</param>
        /// <returns></returns>
        bool Add(Movie movie);

        /// <summary>
        ///电影信息是否存在
        /// </summary>
        /// <param name="name">电影名字</param>
        /// <returns></returns>
        bool IsExist(string name);

        /// <summary>
        ///根据名字获取电影信息
        /// </summary>
        /// <param name="name">电影名字</param>
        /// <returns></returns>
        Movie GetGuidByName(string name);
    }
}