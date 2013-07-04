using System.Text;
using MovieLink.Model;
using MySql.Data.MySqlClient;

namespace MovieLink.Data.MySql
{
    public class DownloadLinkData
    {
        private readonly Dbhelper _helper;

        public DownloadLinkData()
        {
            this._helper = new Dbhelper();
        }

        /// <summary>
        /// 添加下载链接信息
        /// </summary>
        /// <param name="link">下载链接信息</param>
        /// <returns></returns>
        public bool Add(DownloadLink link)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into download(");
            strSql.Append("Guid,LinkAddr,Type,BusinessGuid,Source,SourceName)");
            strSql.Append(" values (");
            strSql.Append("?guid,?link_addr,?type,?business_guid,?source,?source_name)");
            MySqlParameter[] parameters = {
	            new MySqlParameter("?guid", MySqlDbType.VarChar,50),
                new MySqlParameter("?link_addr", MySqlDbType.VarChar,1024),
                new MySqlParameter("?type", MySqlDbType.VarChar,128),
                new MySqlParameter("?business_guid", MySqlDbType.VarChar,50),
                new MySqlParameter("?source", MySqlDbType.VarChar,512),
                new MySqlParameter("?source_name", MySqlDbType.VarChar,128)};
            parameters[0].Value = link.Guid;
            parameters[1].Value = link.LinkAddr;
            parameters[2].Value = link.Type;
            parameters[3].Value = link.BusinessGuid;
            parameters[4].Value = link.Source;
            parameters[5].Value = link.SourceName;
            return _helper.ExecuteNonQuery(strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        ///下载链接是否存在
        /// </summary>
        /// <param name="linkAddr">链接地址</param>
        /// <returns></returns>
        public bool IsExist(string linkAddr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from ");
            strSql.Append(" download ");
            strSql.Append(" where LinkAddr=?link_addr LIMIT 1");
            MySqlParameter[] parameters = {
                new MySqlParameter("?link_addr", MySqlDbType.VarChar,50)};
            parameters[0].Value = linkAddr.Trim();
            object count = _helper.ExecuteScalar(strSql.ToString(), parameters);
            int ret = 0;
            if (count != null)
                int.TryParse(count.ToString(), out ret);
            return ret > 0;
        }
    }
}
