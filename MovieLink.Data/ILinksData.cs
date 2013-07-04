using System.Collections.Generic;

namespace MovieLink.Data
{
    public interface ILinksData
    {
        /// <summary>
        /// 添加链接到数据库
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="link">链接</param>
        /// <returns></returns>
        void AddLink(string type, string link);

        /// <summary>
        /// 批量添加链接到数据库
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="links">链接</param>
        /// <returns></returns>
        void AddLinks(string type, List<string> links);

        /// <summary>
        /// 获取尚未解析的链接信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="count">个数</param>
        /// <returns></returns>
        List<string> GetNotParseLink(string type,int count);

        /// <summary>
        /// 尚未解析的链接个数
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        int GetNotParseLinkCount(string type);

        /// <summary>
        /// 重置上一次没有完成的任务
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        void ResetNotParseLinks(string type);
    }
}