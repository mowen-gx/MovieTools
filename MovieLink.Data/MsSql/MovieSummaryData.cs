using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MovieLink.Data.MsSql
{
    public class MovieSummaryData : IMovieSummaryData
    {
        /// <summary>
        /// 添加电影简介到数据库
        /// </summary>
        /// <param name="movieGuid"></param>
        /// <param name="summary"></param>
        /// <returns></returns>
        public void AddSummary(string movieGuid, string summary)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"IF NOT EXISTS(SELECT * FROM MovieSummary(nolock) WHERE MovieGuid = @MovieGuid)                          
                            BEGIN
                            INSERT INTO MovieSummary (MovieGuid, Summary) VALUES(@MovieGuid, @Summary)
                            END");
            SqlParameter[] parameters = {
	            new SqlParameter("@MovieGuid", SqlDbType.NVarChar,50){Value = movieGuid.Trim()},
                new SqlParameter("@Summary", SqlDbType.Text,10000000){Value = summary.Trim()}};
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(),CommandType.Text, strSql.ToString(), parameters);
        }
    }
}
