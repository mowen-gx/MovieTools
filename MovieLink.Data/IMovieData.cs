using MovieLink.Model;

namespace MovieLink.Data
{
    public interface IMovieData
    {
        /// <summary>
        /// ��ӵ�Ӱ��Ϣ
        /// </summary>
        /// <param name="movie">��Ӱ��Ϣ</param>
        /// <returns></returns>
        bool Add(Movie movie);

        /// <summary>
        ///��Ӱ��Ϣ�Ƿ����
        /// </summary>
        /// <param name="name">��Ӱ����</param>
        /// <returns></returns>
        bool IsExist(string name);

        /// <summary>
        ///�������ֻ�ȡ��Ӱ��Ϣ
        /// </summary>
        /// <param name="name">��Ӱ����</param>
        /// <returns></returns>
        Movie GetGuidByName(string name);
    }
}