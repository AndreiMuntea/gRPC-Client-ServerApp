using System.Data;
using ServerCSharp.DbUtils;

namespace ServerCSharp.Repository.DBRepository.DBConnection
{
   public static class DatabaseConnection
   {
      private static IDbConnection _connection = null;

      private static IDbConnection NewConnection()
      {
         return ConnectionFactory.GetInstance().CreateConnection();
      }
      public static IDbConnection GetConnection()
      {
         if (_connection == null || _connection.State == ConnectionState.Closed)
         {
            _connection = NewConnection();
            _connection.Open();;
         }
         return _connection;
      }
   }
}
