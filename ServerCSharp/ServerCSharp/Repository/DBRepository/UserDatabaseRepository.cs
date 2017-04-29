using System;
using System.Collections.Generic;
using System.Data;
using ServerCSharp.Domain;
using ServerCSharp.Repository.DBRepository.DBConnection;
using ServerCSharp.Repository.Exceptions;
using ServerCSharp.Repository.Interface;

namespace ServerCSharp.Repository.DBRepository
{
   public class UserDatabaseRepository : IUserRepository
   {
      public String TableName { get; set; }
      public QueryBuilder QueryBuilder { get; set; }

      public UserDatabaseRepository()
      {
      }

      public UserDatabaseRepository(string tableName, QueryBuilder queryBuilder)
      {
         TableName = tableName;
         QueryBuilder = queryBuilder;
      }

      public void AddItem(User item)
      {
         if (ExistsItem(item.UserName)) throw new RepositoryExceptionDuplicateEntry("Duplicate entry for user!\n");

         Dictionary<String, String> mappedObject = MapObject(item);
         String addQuery = QueryBuilder.CreateInsertQuery(TableName, mappedObject);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(addQuery, mappedObject);
         if(affectedRows.Equals(0)) throw new RepositoryException("No user inserted!\n");
      }

      public void DeleteItem(string id)
      {
         if (!ExistsItem(id)) throw new RepositoryException("User not found!\n");

         Dictionary<String, String> mappedId = MapId(id);
         String deleteQuery = QueryBuilder.CreateDeleteQuery(TableName, mappedId);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(deleteQuery, mappedId);
         if (affectedRows.Equals(0)) throw new RepositoryException("No user deleted!\n");
      }
      public bool ExistsItem(string id)
      {
         Dictionary<String, String> mappedId = MapId(id);
         String getQuery = QueryBuilder.CreateExistsQuery(TableName, mappedId);
         Int32 result = QueryBuilder.ExecuteScalar<Int32>(getQuery, mappedId, Convert.ToInt32);
         return result != 0;
      }

      public User GetItem(string id)
      {
         if (!ExistsItem(id)) throw new RepositoryException("User not found!\n");
         Dictionary<String, String> mappedId = MapId(id);
         String getQuery = QueryBuilder.CreateGetItemQuery(TableName, mappedId);
         return QueryBuilder.ExecuteReader<User>(getQuery, mappedId, Converter)[0];
      }

      public void UpdateItem(User updatedItem)
      {
         if (!ExistsItem(updatedItem.UserName)) throw new RepositoryException("User not found!\n");

         Dictionary<String, String> mappedId = MapId(updatedItem.UserName);
         Dictionary<String, String> mappedObject = MapObject(updatedItem);
         String updateQuery = QueryBuilder.CreateUpdateQuery(TableName, mappedId, mappedObject);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(updateQuery, mappedId);
         if (affectedRows.Equals(0)) throw new RepositoryException("No user updated!\n");
      }

      public List<User> GetAll()
      {
         String getQuery = QueryBuilder.CreateGetAllQuery(TableName);
         Dictionary<String, String> arguments = new Dictionary<string, string>();
         return QueryBuilder.ExecuteReader<User>(getQuery, arguments, Converter);
      }

      public bool LogIn(User user)
      {
         try
         {
            User existingUser = GetItem(user.UserName);
            return (existingUser.Password.Equals(user.Password));
         }
         catch (RepositoryException)
         {
            return false;
         }
      }


      private User Converter(IDataReader dataReader)
      {
         var userName = dataReader.GetString(0);
         var password = dataReader.GetString(1);
         return new User(userName, password);
      }

      private Dictionary<String, String> MapObject(User item)
      {
         Dictionary<String, String> mappedObject = new Dictionary<string, string>
         {
            {"userName", item.UserName},
            {"password", item.Password}
         };
         return mappedObject;
      }

      private Dictionary<String, String> MapId(String id)
      {
         Dictionary<String, String> mappedObject = new Dictionary<string, string> {{"username", id}};
         return mappedObject;
      }
   }
}
