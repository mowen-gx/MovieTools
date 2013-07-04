using MovieLink.Model;

namespace MovieLink.Data
{
    public interface IDownloadLinkData
    {
        /// <summary>
        /// 添加下载链接信息
        /// </summary>
        /// <param name="link">下载链接信息</param>
        /// <returns></returns>
        bool Add(DownloadLink link);

        /// <summary>
        ///下载链接是否存在
        /// </summary>
        /// <param name="linkAddr">链接地址</param>
        /// <returns></returns>
        bool IsExist(string linkAddr);
    }
}