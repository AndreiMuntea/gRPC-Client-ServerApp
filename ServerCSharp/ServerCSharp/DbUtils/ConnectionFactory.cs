using System;
using System.Data;
using System.Reflection;

namespace ServerCSharp.DbUtils
{
   public abstract class ConnectionFactory
   {
      //constructor
      protected ConnectionFactory()
      {

      }

      //instance
      private static ConnectionFactory _instance;

      //returns the factory
      public static ConnectionFactory GetInstance()
      {
         if (_instance != null) return _instance;

         Assembly assem = Assembly.GetExecutingAssembly();
         Type[] types = assem.GetTypes();
         foreach (Type t in types)
         {
            if (t.IsSubclassOf(typeof(ConnectionFactory)))
               _instance = (ConnectionFactory)Activator.CreateInstance(t);
         }
         return _instance;
      }

      //connection create method
      public abstract IDbConnection CreateConnection();
   }
}
