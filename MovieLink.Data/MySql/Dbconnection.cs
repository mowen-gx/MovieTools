using MySql.Data.MySqlClient;

namespace MovieLink.Data.MySql
{
    public class Dbconnection
    {
        public MySqlConnection DbConn;
        public string ConnectionStr;
        public Dbconnection()
        {
            this.ConnectionStr = "Server=127.0.0.1;Port=3306;User Id=root;Password=123456;Persist Security Info=True;Database=downlink;Pooling=true; Max Pool Size=29999;";
        }
        public MySqlConnection Create()
        {
            this.DbConn = new MySqlConnection(this.ConnectionStr);
            return this.DbConn;
        }
    }
}
