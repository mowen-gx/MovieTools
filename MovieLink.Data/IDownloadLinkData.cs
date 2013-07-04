using MovieLink.Model;

namespace MovieLink.Data
{
    public interface IDownloadLinkData
    {
        /// <summary>
        /// �������������Ϣ
        /// </summary>
        /// <param name="link">����������Ϣ</param>
        /// <returns></returns>
        bool Add(DownloadLink link);

        /// <summary>
        ///���������Ƿ����
        /// </summary>
        /// <param name="linkAddr">���ӵ�ַ</param>
        /// <returns></returns>
        bool IsExist(string linkAddr);
    }
}