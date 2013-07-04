using System.Text;
using MovieLink.Model;
using MySql.Data.MySqlClient;

namespace MovieLink.Data.MySql
{
    public class MovieOtherNameData
    {
        private Dbhelper _helper = new Dbhelper();


        public bool AddMovieOtherName(MovieName movieName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into movieothername(");
            strSql.Append("MovieGuid,`name`)");
            strSql.Append(" values (");
            strSql.Append("?movie_guid,?name)");
            strSql.Append(" ON DUPLICATE KEY UPDATE `name`=?name  ");
            MySqlParameter[] parameters = {
	            new MySqlParameter("?movie_guid", MySqlDbType.VarChar,50),
                new MySqlParameter("?name", MySqlDbType.VarChar,512)};
            parameters[0].Value = movieName.MovieGuid;
            parameters[1].Value = movieName.Name;
            return _helper.ExecuteNonQuery(strSql.ToString(), parameters) > 0;

        }

        /// <summary>
        ///电影别名信息是否存在
        /// </summary>
        /// <param name="movieName">电影别名信息</param>
        /// <returns></returns>
        public bool IsExist(MovieName movieName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from ");
            strSql.Append(" movieothername ");
            strSql.Append(" where `MovieGuid`=?movie_guid and `name`=?name");
            MySqlParameter[] parameters = {
                new MySqlParameter("?movie_guid", MySqlDbType.VarChar,50),
                new MySqlParameter("?name", MySqlDbType.VarChar,512)};
            parameters[0].Value = movieName.MovieGuid.Trim();
            parameters[1].Value = movieName.Name.Trim();
            object count = _helper.ExecuteScalar(strSql.ToString(), parameters);
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
            strSql.Append("select * from ");
            strSql.Append(" movieothername ");
            strSql.Append(" where `name`=?name LIMIT 1");
            MySqlParameter[] parameters = {
                new MySqlParameter("?name", MySqlDbType.VarChar,50)};
            parameters[0].Value = name.Trim();
            MySqlDataReader reader = _helper.ExecuteReader(strSql.ToString(), parameters);
            if (reader != null)
            {
                if (reader.Read())
                {
                    model = new MovieName();
                    model.MovieGuid = reader.GetString("MovieGuid");
                    model.Name = reader.GetString("name");
                }
                reader.Close();
            }
            return model;
        }
    }
}
