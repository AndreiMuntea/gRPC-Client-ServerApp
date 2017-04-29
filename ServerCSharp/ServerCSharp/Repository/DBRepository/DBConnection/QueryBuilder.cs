using System;
using System.Collections.Generic;
using System.Data;

namespace ServerCSharp.Repository.DBRepository.DBConnection
{
   public class QueryBuilder
   {
      private static QueryBuilder _instance = null;

      private QueryBuilder()
      {
      }

      public static QueryBuilder GetInstance()
      {
         return _instance ?? (_instance = new QueryBuilder());
      }

      public String CreateInsertQuery(String tableName,
         Dictionary<String, String> mappedObject)
      {
         String queryInsert = "INSERT INTO " + tableName;
         String columns = "(";
         String values = "VALUES(";

         foreach (var keyValuePair in mappedObject)
         {
            columns += keyValuePair.Key + ",";
            values += "@" + keyValuePair.Key + ",";
         }
         columns = columns.Substring(0, columns.Length - 1) + ")";
         values = values.Substring(0, values.Length - 1) + ")";

         return queryInsert + " " + columns + " " + values;
      }

      public String CreateDeleteQuery(String tableName,
         Dictionary<String, String> mappedId)
      {
         String queryDelete = "DELETE FROM " + tableName + " WHERE";
         String args = "";

         foreach (var keyValuePair in mappedId)
         {
            args += keyValuePair.Key + "=@" + keyValuePair.Key + "AND ";
         }
         args = args.Substring(0, args.Length - 4);

         return queryDelete + " " + args;
      }

      public String CreateUpdateQuery(String tableName,
         Dictionary<String, String> mappedId,
         Dictionary<String, String> mappedObject)
      {
         String queryUpdate = "UPDATE " + tableName;
         String filterCriteria = "WHERE ";
         String updateFields = "SET ";

         foreach (var keyValuePair in mappedId)
         {
            filterCriteria += keyValuePair.Key + "=@" + keyValuePair + "AND ";
         }
         filterCriteria = filterCriteria.Substring(0, filterCriteria.Length - 4);

         foreach (var keyValuePair in mappedObject)
         {
            updateFields += keyValuePair.Key + "=@" + keyValuePair.Key + ",";
         }
         updateFields = updateFields.Substring(0, updateFields.Length - 1);

         return queryUpdate + " " + updateFields + " " + filterCriteria;
      }

      public String CreateGetItemQuery(String tableName, Dictionary<String, String> mappedId)
      {
         String queryGet = "SELECT * FROM " + tableName;
         String filterCriteria = "WHERE ";
         foreach (var keyValuePair in mappedId)
         {
            filterCriteria += keyValuePair.Key + "=@" + keyValuePair.Key + "AND ";
         }
         filterCriteria = filterCriteria.Substring(0, filterCriteria.Length - 4);

         return queryGet + " " + filterCriteria;
      }

      public String CreateGetAllQuery(String tableName)
      {
         String queryGetAll = "SELECT * FROM " + tableName;
         return queryGetAll;
      }

      public String CreateExistsQuery(String tableName, Dictionary<String, String> filters)
      {
         String queryExists = "SELECT EXISTS( SELECT * FROM " + tableName;
         String filterCriteria = "WHERE ";

         foreach (var keyValuePair in filters)
         {
            filterCriteria += keyValuePair.Key + "=@" + keyValuePair.Key + "AND ";
         }
         filterCriteria = filterCriteria.Substring(0, filterCriteria.Length - 4);

         queryExists += " " + filterCriteria + ")";
         return queryExists;
      }

      private void PrepareArguments(IDbCommand command, Dictionary<String, String> arguments)
      {
         foreach (var keyValuePair in arguments)
         {
            var parameter = command.CreateParameter();
            parameter.ParameterName = "@" + keyValuePair.Key;
            parameter.Value = keyValuePair.Value;
            command.Parameters.Add(parameter);
         }
      }

      public Int32 ExecuteNonQuery(String query, Dictionary<String, String> queryArguments)
      {
         var connection = DatabaseConnection.GetConnection();
         Int32 affectedRows;

         using (var command = connection.CreateCommand())
         {
            command.CommandText = query;
            PrepareArguments(command, queryArguments);
            affectedRows = command.ExecuteNonQuery();
         }
         return affectedRows;
      }

      public T ExecuteScalar<T>(String query, Dictionary<String, String> queryArguments, Func<object, T> converter )
      {
         var connection = DatabaseConnection.GetConnection();
         object returned;

         using (var command = connection.CreateCommand())
         {
            command.CommandText = query;
            PrepareArguments(command, queryArguments);
            returned = command.ExecuteScalar();
         }
         return converter(returned);
      }

      public List<T> ExecuteReader<T>(String query, Dictionary<String, String> queryArguments, Func<IDataReader, T> converter)
      {
         var connection = DatabaseConnection.GetConnection();
         List<T> allItems = new List<T>();

         using (var command = connection.CreateCommand())
         {
            command.CommandText = query;
            PrepareArguments(command, queryArguments);

            using (var dataReader = command.ExecuteReader())
            {
               while (dataReader.Read())
               {
                  allItems.Add(converter(dataReader));
               }
            }
         }
         return allItems;
      }
   }
}