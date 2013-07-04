using System.Data;
using System.Data.SqlClient;
using System.Text;
using MovieLink.Model;

namespace MovieLink.Data.MsSql
{
    public class MovieNamesData : IMovieOtherNameData
    {
        public bool AddMovieOtherName(MovieName movieName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO MovieNames(");
            strSql.Append("MovieGuid,Name)");
            strSql.Append(" VALUES (");
            strSql.Append("@MovieGuid,@Name)");
            SqlParameter[] parameters = {
	            new SqlParameter("@MovieGuid", SqlDbType.VarChar,50){Value = movieName.MovieGuid},
                new SqlParameter("@Name", SqlDbType.VarChar,512){Value = movieName.Name}};
            return SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(),CommandType.Text, strSql.ToString(), parameters) > 0;

        }

        /// <summary>
        ///电影别名信息是否存在
        /// </summary>
        /// <param name="movieName">电影别名信息</param>
        /// <returns></returns>
        public bool IsExist(MovieName movieName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(*) FROM ");
            strSql.Append(" MovieNames(nolock) ");
            strSql.Append(" WHERE MovieGuid=@MovieGuid and Name=@Name");
            SqlParameter[] parameters = {
                new SqlParameter("@MovieGuid", SqlDbType.VarChar,50){Value =movieName.MovieGuid.Trim() },
                new SqlParameter("@Name", SqlDbType.VarChar,512){Value =movieName.Name.Trim() }};
            object count = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text,strSql.ToString(), parameters);
            int ret = 0;
            if (count != null)
                int.TryParse(count.ToString(), out ret);
            return ret > 0;
        }

        /// <summary>
        ///根据名字获取电影信息
        /// </summary>
        /// <param name="name">电影名字</param>
        /// <returns></returns>
        public MovieName GetByName(string name)
        {
            MovieName model = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(" MovieNames(nolock) ");
            strSql.Append(" where Name=@Name");
            SqlParameter[] parameters = {
                new SqlParameter("@Name", SqlDbType.NVarChar,50){Value =name.Trim() }};
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text,strSql.ToString(), parameters);
            if (reader != null)
            {
                if (reader.Read())
                {
                    model = new MovieName {MovieGuid = reader["MovieGuid"].ToString(), Name = reader["Name"].ToString()};
                }
                reader.Close();
            }
            return model;
        }
    }
}
