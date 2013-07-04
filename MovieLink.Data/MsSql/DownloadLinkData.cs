using System.Data;
using System.Data.SqlClient;
using System.Text;
using MovieLink.Model;

namespace MovieLink.Data.MsSql
{
    public class DownloadLinkData : IDownloadLinkData
    {
        /// <summary>
        /// 添加下载链接信息
        /// </summary>
        /// <param name="link">下载链接信息</param>
        /// <returns></returns>
        public bool Add(DownloadLink link)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO DownloadLink(");
            strSql.Append("Guid,LinkAddr,BusinessGuid,Source,SourceName)");
            strSql.Append(" VALUES (");
            strSql.Append("@Guid,@LinkAddr,@BusinessGuid,@Source,@SourceName)");
            SqlParameter[] parameters = {
	            new SqlParameter("@Guid", SqlDbType.NVarChar,50){Value = link.Guid},
                new SqlParameter("@LinkAddr", SqlDbType.NVarChar,512){Value = link.LinkAddr},
                new SqlParameter("@BusinessGuid", SqlDbType.NVarChar,50){Value = link.BusinessGuid},
                new SqlParameter("@Source", SqlDbType.NVarChar,512){Value = link.Source},
                new SqlParameter("@SourceName", SqlDbType.NVarChar,128){Value = link.SourceName}};
            return SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(),CommandType.Text,strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        ///下载链接是否存在
        /// </summary>
        /// <param name="linkAddr">链接地址</param>
        /// <returns></returns>
        public bool IsExist(string linkAddr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(Guid) FROM ");
            strSql.Append(" DownloadLink(nolock) ");
            strSql.Append(" WHERE LinkAddr=@LinkAddr");
            SqlParameter[] parameters = {
                new SqlParameter("@LinkAddr", SqlDbType.NVarChar,50){Value = linkAddr.Trim()}};
            object count = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(),CommandType.Text,strSql.ToString(), parameters);
            int ret = 0;
            if (count != null)
                int.TryParse(count.ToString(), out ret);
            return ret > 0;
        }
    }
}
