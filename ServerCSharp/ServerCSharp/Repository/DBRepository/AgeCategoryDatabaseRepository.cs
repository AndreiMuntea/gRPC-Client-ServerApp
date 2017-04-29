using System;
using System.Collections.Generic;
using System.Data;
using ServerCSharp.Domain;
using ServerCSharp.Repository.DBRepository.DBConnection;
using ServerCSharp.Repository.Exceptions;
using ServerCSharp.Repository.Interface;

namespace ServerCSharp.Repository.DBRepository
{
   public class AgeCategoryDatabaseRepository : IAgeCategoryRepository
   {

      public String TableName { get; set; }
      public QueryBuilder QueryBuilder { get; set; }

      public AgeCategoryDatabaseRepository()
      {
      }

      public AgeCategoryDatabaseRepository(string tableName, QueryBuilder queryBuilder)
      {
         TableName = tableName;
         QueryBuilder = queryBuilder;
      }


      public void AddItem(AgeCategory item)
      {
         if (ExistsItem(item.Name)) throw new RepositoryExceptionDuplicateEntry("Duplicate Entry for AgeCategory!\n");

         Dictionary<String, String> mappedObject = MapObject(item);
         String addQuery = QueryBuilder.CreateInsertQuery(TableName, mappedObject);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(addQuery, mappedObject);
         if (affectedRows.Equals(0)) throw new RepositoryException("No age category inserted!\n");
      }

      public void DeleteItem(String id)
      {
         if (!ExistsItem(id)) throw new RepositoryException("No age category found!\n");

         Dictionary<String, String> mappedId = MapId(id);
         String deleteQuery = QueryBuilder.CreateDeleteQuery(TableName, mappedId);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(deleteQuery, mappedId);
         if (affectedRows.Equals(0)) throw new RepositoryException("No age category deleted!\n");
      }

      public bool ExistsItem(String id)
      {
         Dictionary<String, String> mappedId = MapId(id);
         String getQuery = QueryBuilder.CreateExistsQuery(TableName, mappedId);
         Int32 result = QueryBuilder.ExecuteScalar<Int32>(getQuery, mappedId, Convert.ToInt32);
         return result != 0;
      }

      public AgeCategory GetItem(String id)
      {
         if (!ExistsItem(id)) throw new RepositoryException("No age category found!\n");

         Dictionary<String, String> mappedId = MapId(id);
         String getQuery = QueryBuilder.CreateGetItemQuery(TableName, mappedId);
         return QueryBuilder.ExecuteReader<AgeCategory>(getQuery, mappedId, Converter)[0];
      }


      public void UpdateItem(AgeCategory updatedItem)
      {
         if (!ExistsItem(updatedItem.Name)) throw new RepositoryException("No age category found!\n");

         Dictionary<String, String> mappedId = MapId(updatedItem.Name);
         Dictionary<String, String> mappedObject = MapObject(updatedItem);
         String updateQuery = QueryBuilder.CreateUpdateQuery(TableName, mappedId, mappedObject);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(updateQuery, mappedId);
         if (affectedRows.Equals(0)) throw new RepositoryException("No age category updated!\n");
      }

      public List<AgeCategory> GetAll()
      {
         String getQuery = QueryBuilder.CreateGetAllQuery(TableName);
         Dictionary<String, String> arguments = new Dictionary<string, string>();
         return QueryBuilder.ExecuteReader<AgeCategory>(getQuery, arguments, Converter);
      }

      public AgeCategory FindSuitableAgeCategory(int age)
      {
         String query = "SELECT categoryName,minAge,maxAge FROM ageCategories " +
                        "WHERE minAge<=@age AND maxAge>=@age";
         Dictionary<String, String> arguments = new Dictionary<string, string> {{"age", age.ToString()}};
         List<AgeCategory> allSuitableCategories = QueryBuilder.ExecuteReader<AgeCategory>(query, arguments, Converter);
         if (allSuitableCategories.Count == 0) throw new RepositoryException("No suitable age category found!\n");
         return allSuitableCategories[0];
      }

      public AgeCategory FindSuitableAgeCategory(int minAge, int maxAge)
      {
         String query = "SELECT categoryName,minAge,maxAge FROM ageCategories " +
               "WHERE minAge=@minAge AND maxAge=@maxAge";
         Dictionary<String, String> arguments = new Dictionary<string, string> { { "minAge", minAge.ToString() }, {"maxAge",maxAge.ToString()} };
         List<AgeCategory> allSuitableCategories = QueryBuilder.ExecuteReader<AgeCategory>(query, arguments, Converter);
         if (allSuitableCategories.Count == 0) throw new RepositoryException("No suitable age category found!\n");
         return allSuitableCategories[0];
      }


      private AgeCategory Converter(IDataReader dataReader)
      {
         var idAgeCategory = dataReader.GetString(0);
         var minAge = dataReader.GetInt32(1);
         var maxAge = dataReader.GetInt32(2);
         return new AgeCategory(idAgeCategory,minAge, maxAge);
      }

      private Dictionary<String, String> MapObject(AgeCategory item)
      {
         Dictionary<String, String> mappedObject = new Dictionary<string, string>();
         mappedObject.Add("categoryName", item.Name);
         mappedObject.Add("minAge", item.MinAge.ToString());
         mappedObject.Add("maxAge", item.MaxAge.ToString());
         return mappedObject;
      }

      private Dictionary<String, String> MapId(String id)
      {
         Dictionary<String, String> mappedObject = new Dictionary<string, string>();
         mappedObject.Add("categoryName", id);
         return mappedObject;
      }
   }
}
