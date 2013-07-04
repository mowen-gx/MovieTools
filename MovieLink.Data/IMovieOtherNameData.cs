using MovieLink.Model;

namespace MovieLink.Data
{
    public interface IMovieOtherNameData
    {
        bool AddMovieOtherName(MovieName movieName);

        /// <summary>
        ///��Ӱ������Ϣ�Ƿ����
        /// </summary>
        /// <param name="movieName">��Ӱ������Ϣ</param>
        /// <returns></returns>
        bool IsExist(MovieName movieName);

        /// <summary>
        ///�������ֻ�ȡ��Ӱ��Ϣ
        /// </summary>
        /// <param name="name">��Ӱ����</param>
        /// <returns></returns>
        MovieName GetByName(string name);
    }
}