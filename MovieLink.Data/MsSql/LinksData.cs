using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MovieLink.Data.MsSql
{
    public class LinksData : ILinksData
    {
        /// <summary>
        /// 添加链接到数据库
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="link">链接</param>
        /// <returns></returns>
        public void AddLink(string type, string link)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"IF NOT EXISTS(SELECT * FROM Links(nolock) WHERE LinkAddr = @LinkAddr)                          
                            BEGIN
                            INSERT INTO Links (Guid, LinkAddr,Type) VALUES(@Guid, @LinkAddr,@Type)
                            END");
            SqlParameter[] parameters = {
	            new SqlParameter("@Guid", SqlDbType.NVarChar,50){Value = Guid.NewGuid().ToString()},
                new SqlParameter("@LinkAddr", SqlDbType.NVarChar,512){Value = link.Trim()},
                new SqlParameter("@Type", SqlDbType.NVarChar,512){Value = type.Trim()}};
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(),CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 批量添加链接到数据库
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="links">链接</param>
        /// <returns></returns>
        public void AddLinks(string type, List<string> links)
        {
            if(links.Count < 1)
                return;
            StringBuilder strSql = new StringBuilder();
            foreach (string link in links)
            {
                strSql.Append(@" IF NOT EXISTS(SELECT * FROM Links(nolock) WHERE LinkAddr = '"+link+"') " +                   
                                 @"BEGIN
                                    INSERT INTO Links (Guid, LinkAddr,Type,GetGuid) VALUES('" + Guid.NewGuid().ToString() + "', '" + link +
                                 @"','"+ type.Trim()+"','')END");
            }
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(),CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获取尚未解析的链接信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count">个数</param>
        /// <returns></returns>
        public List<string> GetNotParseLink(string type, int count)
        {
            List<string> links = new List<string>();
            StringBuilder strSql = new StringBuilder();
            string guid = Guid.NewGuid().ToString();
            strSql.Append("Update Links SET GetGuid='" + guid + "' WHERE GUID IN (SELECT TOP " + count +
                          " GUID FROM Links(nolock) WHERE IsParse=0 AND [Type]=@Type AND (GetGuid IS NULL OR GetGuid =''))");
            strSql.Append("SELECT TOP " + count + " * FROM Links(nolock) WHERE IsParse=0 AND [Type]=@Type AND GetGuid='" + guid + "'");
            SqlParameter[] parameters = {
                new SqlParameter("@Type", SqlDbType.NVarChar,512){Value = type.Trim()}};
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString(), parameters);
            while (reader.Read())
            {
                links.Add(reader["LinkAddr"].ToString());
            }
            reader.Close();
            return links;
        }

        /// <summary>
        /// 尚未解析的链接个数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetNotParseLinkCount(string type)
        {
            int count = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(Guid) CCOUNT FROM Links(nolock) WHERE IsParse=0  AND Type=@Type");
            SqlParameter[] parameters = {
                new SqlParameter("@Type", SqlDbType.NVarChar,512){Value = type.Trim()}};
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString(), parameters);
            while (reader.Read())
            {
                int.TryParse(reader["CCOUNT"].ToString(), out count);
            }
            reader.Close();
            return count;
        }

        /// <summary>
        /// 设置已完成解析的链接
        /// </summary>
        /// <returns></returns>
        public void SetDoneParseLink(List<string> links)
        {
            if (links.Count < 1)
                return;
            StringBuilder strSql = new StringBuilder();
            foreach (string link in links)
            {
                strSql.Append(@" UPDATE Links SET IsParse = 1 WHERE LinkAddr = '" + link + "';");
            }
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 设置已完成解析的链接
        /// </summary>
        /// <returns></returns>
        public void SetDoneAddToDbLink(List<string> links)
        {
            if (links.Count < 1)
                return;
            StringBuilder strSql = new StringBuilder();
            foreach (string link in links)
            {
                strSql.Append(@" UPDATE Links SET IsAddToDb = 1 WHERE LinkAddr = '" + link + "';");
            }
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 重置上一次没有完成的任务
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public void ResetNotParseLinks(string type) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" UPDATE Links SET GetGuid = '' WHERE IsParse = 0 AND GetGuid IS NOT NULL AND [Type]=@Type");
            SqlParameter[] parameters = {
                new SqlParameter("@Type", SqlDbType.NVarChar,512){Value = type.Trim()}};
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString(), parameters);
        }
    }
}
