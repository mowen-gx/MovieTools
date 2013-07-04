using System.Text;
using MovieLink.Model;
using MySql.Data.MySqlClient;

namespace MovieLink.Data.MySql
{
    public class MovieData
    {
        private Dbhelper _helper = new Dbhelper();
        
        /// <summary>
        /// 添加电影信息
        /// </summary>
        /// <param name="movie">电影信息</param>
        /// <returns></returns>
        public bool Add(Movie movie)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into movie(");
            strSql.Append("Guid,Name,Country,Language,ScreenWriter,Summary,MainPic,Publish)");
            strSql.Append(" values (");
            strSql.Append("?guid,?name,?country,?language,?screenwriter,?summary,?main_pic,?publish)");
            MySqlParameter[] parameters = {
	            new MySqlParameter("?guid", MySqlDbType.VarChar,50),
                new MySqlParameter("?name", MySqlDbType.VarChar,512),
                new MySqlParameter("?country", MySqlDbType.VarChar,512),
                new MySqlParameter("?language", MySqlDbType.VarChar,512),
                new MySqlParameter("?screenwriter", MySqlDbType.VarChar,512),
                new MySqlParameter("?summary", MySqlDbType.VarChar,8192),
                new MySqlParameter("?main_pic", MySqlDbType.VarChar,512),
                new MySqlParameter("?publish", MySqlDbType.VarChar,512)};
            parameters[0].Value = movie.Guid;
            parameters[1].Value = movie.Name;
            parameters[2].Value = movie.Country;
            parameters[3].Value = movie.Language;
            parameters[4].Value = movie.Screenwriter;
            if (movie.Summary.Length > 8000)
                movie.Summary = movie.Summary.Substring(0, 8000);
            parameters[5].Value = movie.Summary;
            parameters[6].Value = movie.MainPic;
            parameters[7].Value = movie.Publish;
            return _helper.ExecuteNonQuery(strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        ///电影信息是否存在
        /// </summary>
        /// <param name="name">电影名字</param>
        /// <returns></returns>
        public bool IsExist(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from ");
            strSql.Append(" movie ");
            strSql.Append(" where Name=?name");
            MySqlParameter[] parameters = {
                new MySqlParameter("?name", MySqlDbType.VarChar,50)};
            parameters[0].Value = name.Trim();
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
        public Movie GetGuidByName(string name)
        {
            Movie model = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Guid,Name from ");
            strSql.Append(" movie ");
            strSql.Append(" where Name=?name LIMIT 1");
            MySqlParameter[] parameters = {
                new MySqlParameter("?name", MySqlDbType.VarChar,50)};
            parameters[0].Value = name.Trim();
            MySqlDataReader reader = _helper.ExecuteReader(strSql.ToString(), parameters);
            if (reader != null)
            {
                if (reader.Read())
                {
                    model = new Movie();
                    model.Guid = reader.GetString("Guid");
                    model.Name = reader.GetString("Name");
                    //model.MainPic = reader.GetString("MainPic");
                    //model.Summary = reader.GetString("Summary");
                }
                reader.Close();
            }
            return model;
        }
    }
}
