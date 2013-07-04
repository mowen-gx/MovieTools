using MySql.Data.MySqlClient;

namespace MovieLink.Data.MySql
{
    public class Dbhelper
    {
        private readonly string _connStr;
        
        public Dbhelper()
        {
            Dbconnection dbconn = new Dbconnection();
            this._connStr = dbconn.ConnectionStr;
        }
        
        public MySqlDataReader ExecuteReader(string commandText)
        {
            return MySqlHelper.ExecuteReader(_connStr, commandText);
        }


        public MySqlDataReader ExecuteReader(string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteReader(_connStr, commandText, commandParameters);
        }


        public object ExecuteScalar(string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteScalar(_connStr, commandText, commandParameters);
        }


        public object ExecuteScalar(string commandText)
        {
            return MySqlHelper.ExecuteScalar(_connStr, commandText);
        }


        public int ExecuteNonQuery(string commandText, params MySqlParameter[] parms)
        {
            return MySqlHelper.ExecuteNonQuery(_connStr, commandText, parms);
        }
    }
}
