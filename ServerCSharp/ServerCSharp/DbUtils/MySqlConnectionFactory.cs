using System.Data;
using MySql.Data.MySqlClient;

namespace ServerCSharp.DbUtils
{
   public class MySqlConnectionFactory : ConnectionFactory
   {
      public MySqlConnectionFactory()
      {
      }

      public override IDbConnection CreateConnection()
      {
         return new MySqlConnection("Database=concurs;" +
                                   "Data Source=localhost;" +
                                   "User id=root;" +
                                   "Password=test;");
      }
   }
}
