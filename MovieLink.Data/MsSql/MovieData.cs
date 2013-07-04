using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using MovieLink.Model;
using Type = MovieLink.Model.Type;

namespace MovieLink.Data.MsSql
{
    public class MovieData : IMovieData
    {
        /// <summary>
        /// 添加电影信息
        /// </summary>
        /// <param name="movie">电影信息</param>
        /// <returns></returns>
        public bool Add(Movie movie)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Movie(");
            strSql.Append("Guid,Name,Country,Language,ScreenWriter,MainPic,Publish)");
            strSql.Append(" VALUES (");
            strSql.Append("@Guid,@Name,@Country,@Language,@ScreenWriter,@MainPic,@Publish)");
            SqlParameter[] parameters = {
	            new SqlParameter("@Guid", SqlDbType.NVarChar,50){Value =movie.Guid },
                new SqlParameter("@Name", SqlDbType.NVarChar,512){Value =movie.Name },
                new SqlParameter("@Country", SqlDbType.NVarChar,512){Value =movie.Country??string.Empty },
                new SqlParameter("@Language", SqlDbType.NVarChar,512){Value =movie.Language??string.Empty },
                new SqlParameter("@ScreenWriter", SqlDbType.NVarChar,512){Value =movie.Screenwriter??string.Empty },
                new SqlParameter("@MainPic", SqlDbType.NVarChar,512){Value =movie.MainPic },
                new SqlParameter("@Publish", SqlDbType.NVarChar,512){Value =movie.Publish??string.Empty }};
            return SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(),CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 更新电影信息
        /// </summary>
        /// <param name="movie">电影信息</param>
        /// <returns></returns>
        public bool Update(MovieInfo movie)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Movie SET MainPic=@MainPic,Publish=@Publish,IsSyn=1,Era=@Era");
            strSql.Append(" WHERE ");
            strSql.Append(" Guid=@Guid ");
            SqlParameter[] parameters = {
                new SqlParameter("@MainPic", SqlDbType.NVarChar,512){Value =movie.MainPic },
                new SqlParameter("@Publish", SqlDbType.NVarChar,512){Value =movie.Publish },
                new SqlParameter("@Era", SqlDbType.NVarChar,512){Value =movie.Era },
                new SqlParameter("@Guid", SqlDbType.NVarChar,512){Value =movie.Guid }};
            return SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        ///电影信息是否存在
        /// </summary>
        /// <param name="name">电影名字</param>
        /// <returns></returns>
        public bool IsExist(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(*) FROM ");
            strSql.Append(" Movie(nolock) ");
            strSql.Append(" WHERE Name=@Name");
            SqlParameter[] parameters = {
                new SqlParameter("@Name", SqlDbType.NVarChar,50){Value =name}};
            object count = SqlHelper.ExecuteScalar(SqlHelper.GetConnSting(),CommandType.Text, strSql.ToString(), parameters);
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
            strSql.Append("SELECT Guid,Name,IsSyn FROM ");
            strSql.Append(" Movie(nolock) ");
            strSql.Append(" WHERE Name=@Name");
            SqlParameter[] parameters = {
                new SqlParameter("@Name", SqlDbType.NVarChar,50){Value = name.Trim()}};
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.GetConnection(),CommandType.Text,strSql.ToString(), parameters);
            if (reader != null)
            {
                if (reader.Read())
                {
                    model = new Movie { Guid = reader["Guid"].ToString(), Name = reader["Name"].ToString()  };
                    int isSys = 0;
                    if (reader["IsSyn"] != null)
                        int.TryParse(reader["IsSyn"].ToString(), out isSys);
                    model.IsSyn = isSys;
                }
                reader.Close();
            }
            return model;
        }

        
        /// <summary>
        /// 电影类型
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,Type> GetTypes()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(" Type(nolock) ");
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, strSql.ToString());
            if (reader != null)
            {
                while (reader.Read())
                {
                    Type era = new Type();
                    era.Guid = reader["Guid"].ToString().Trim();
                    era.Name = reader["Name"].ToString().Trim();
                    if (!types.ContainsKey(era.Name))
                    {
                        types.Add(era.Name,era);
                    }
                   
                }
                reader.Close();
            }
            return types;
        }

        public void AddTypes(List<Type> types)
        {
            if (types.Count < 1)
                return;
            StringBuilder strSql = new StringBuilder();
            foreach (Type type in types)
            {
                strSql.Append(@" IF NOT EXISTS(SELECT * FROM Type(nolock) WHERE Name = '" + type.Name.Trim() + "') " +
                                 @"BEGIN
                                    INSERT INTO Type (Guid, Name) VALUES('" + Guid.NewGuid().ToString() + "', '" + type.Name.Trim() +
                                 @"') 
                                END");
            }
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString());
        }

        public bool AddMovieTypes(string movieGuid, List<Type> types)
        {
            StringBuilder strSql = new StringBuilder();
            foreach (Type type in types)
            {
                if (type != null &&!string.IsNullOrEmpty(type.Guid))
                {
                    strSql.Append(@" IF NOT EXISTS(SELECT * FROM MovieType(nolock) WHERE MovieGuid = '" + movieGuid.Trim() + "' AND TypeGuid = '" + type.Guid.Trim() + "') " +
                                @"BEGIN
                                    INSERT INTO MovieType (MovieGuid, TypeGuid) VALUES('" + movieGuid.Trim() + "', '" + type.Guid.Trim() +
                                @"') 
                                END; ");
                }
            }
            return SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString()) > 0;
        }

        /// <summary>
        /// 电影地区 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,Country> GetCountries()
        {
            Dictionary<string, Country> countries = new Dictionary<string, Country>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(" Country(nolock) ");
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, strSql.ToString());
            if (reader != null)
            {
                while (reader.Read())
                {
                    Country era = new Country();
                    era.Guid = reader["Guid"].ToString().Trim();
                    era.Name = reader["Name"].ToString().Trim();
                    if (!countries.ContainsKey(era.Name)) countries.Add(era.Name,era);
                }
                reader.Close();
            }
            return countries;
        }

        public void AddCountries(List<Country> countries)
        {
            if (countries.Count < 1)
                return;
            StringBuilder strSql = new StringBuilder();
            foreach (Country country in countries)
            {
                strSql.Append(@" IF NOT EXISTS(SELECT * FROM Country(nolock) WHERE Name = '" + country.Name.Trim() + "') " +
                                 @"BEGIN
                                    INSERT INTO Country (Guid, Name) VALUES('" + Guid.NewGuid().ToString() + "', '" + country.Name.Trim() +
                                 @"') 
                                END");
            }
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString());
        }

        public bool AddMovieCountries(string movieGuid, List<Country> countries)
        {
            StringBuilder strSql = new StringBuilder();
            foreach (Country country in countries)
            {
                if (country != null && !string.IsNullOrEmpty(country.Guid))
                {
                    strSql.Append(@" IF NOT EXISTS(SELECT * FROM MovieCountry(nolock) WHERE MovieGuid = '" + movieGuid.Trim() + "' AND CountryGuid = '" + country.Guid.Trim() + "') " +
                                @"BEGIN
                                    INSERT INTO MovieCountry (MovieGuid, CountryGuid) VALUES('" + movieGuid.Trim() + "', '" + country.Guid.Trim() +
                                @"') 
                                END; ");
                }
            }
            return SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString()) > 0;
        }


        /// <summary>
        /// 电影语言
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Language> GetLanguages()
        {
            Dictionary<string, Language> languages = new Dictionary<string, Language>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(" Language(nolock) ");
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(), CommandType.Text, strSql.ToString());
            if (reader != null)
            {
                while (reader.Read())
                {
                    Language era = new Language();
                    era.Guid = reader["Guid"].ToString().Trim();
                    era.Name = reader["Name"].ToString().Trim();
                    if (!languages.ContainsKey(era.Name)) languages.Add(era.Name,era);
                }
                reader.Close();
            }
            return languages;
        }

        public void AddLanguages(List<Language> languages)
        {
            if (languages.Count < 1)
                return;
            StringBuilder strSql = new StringBuilder();
            foreach (Language language in languages)
            {
                strSql.Append(@" IF NOT EXISTS(SELECT * FROM Language(nolock) WHERE Name = '" + language.Name.Trim() + "') " +
                                 @"BEGIN
                                    INSERT INTO Language (Guid, Name) VALUES('" + Guid.NewGuid().ToString() + "', '" + language.Name.Trim() +
                                 @"') 
                                END");
            }
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString());
        }

        public bool AddMovieLanguages(string movieGuid, List<Language> countries)
        {
            StringBuilder strSql = new StringBuilder();
            foreach (Language language in countries)
            {
                if (language != null && !string.IsNullOrEmpty(language.Guid))
                {
                    strSql.Append(@" IF NOT EXISTS(SELECT * FROM MovieLanguage(nolock) WHERE  MovieGuid= '" + movieGuid.Trim() + "' AND LanguageGuid = '" + language.Guid.Trim() + "') " +
                                @"BEGIN
                                    INSERT INTO MovieLanguage (MovieGuid, LanguageGuid) VALUES('" + movieGuid.Trim() + "', '" + language.Guid.Trim() +
                                @"') 
                                END; ");
                }
            }
            return SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, strSql.ToString()) > 0;
        }
    }
}
