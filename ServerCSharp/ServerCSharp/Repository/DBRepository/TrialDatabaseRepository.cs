using System;
using System.Collections.Generic;
using System.Data;
using ServerCSharp.Domain;
using ServerCSharp.Repository.DBRepository.DBConnection;
using ServerCSharp.Repository.Exceptions;
using ServerCSharp.Repository.Interface;

namespace ServerCSharp.Repository.DBRepository
{
   public class TrialDatabaseRepository : ITrialRepository
   {
      public String TableName { get; set; }
      public QueryBuilder QueryBuilder { get; set; }
      public TrialDatabaseRepository()
      {
      }

      public TrialDatabaseRepository(string tableName, QueryBuilder queryBuilder)
      {
         TableName = tableName;
         QueryBuilder = queryBuilder;
      }

      public void AddItem(Trial item)
      {
         if (ExistsItem(item.Name)) throw new RepositoryExceptionDuplicateEntry("Duplicate Entry for trial!\n");

         Dictionary<String, String> mappedObject = MapObject(item);
         String addQuery = QueryBuilder.CreateInsertQuery(TableName, mappedObject);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(addQuery, mappedObject);
         if (affectedRows.Equals(0)) throw new RepositoryException("No trial inserted!\n");
      }

      public void DeleteItem(string id)
      {
         if (!ExistsItem(id)) throw new RepositoryException("No trial found!\n");

         Dictionary<String, String> mappedId = MapId(id);
         String deleteQuery = QueryBuilder.CreateDeleteQuery(TableName, mappedId);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(deleteQuery, mappedId);
         if (affectedRows.Equals(0)) throw new RepositoryException("No trial deleted!\n");
      }

      public bool ExistsItem(string id)
      {
         Dictionary<String, String> mappedId = MapId(id);
         String getQuery = QueryBuilder.CreateExistsQuery(TableName, mappedId);
         Int32 result = QueryBuilder.ExecuteScalar<Int32>(getQuery, mappedId, Convert.ToInt32);
         return result != 0;
      }

      public Trial GetItem(string id)
      {
         if (!ExistsItem(id)) throw new RepositoryException("No trial found!\n");

         Dictionary<String, String> mappedId = MapId(id);
         String getQuery = QueryBuilder.CreateGetItemQuery(TableName, mappedId);
         return QueryBuilder.ExecuteReader<Trial>(getQuery, mappedId, Converter)[0];
      }

      public void UpdateItem(Trial updatedItem)
      {
         if (!ExistsItem(updatedItem.Name)) throw new RepositoryException("No trial found!\n");

         Dictionary<String, String> mappedId = MapId(updatedItem.Name);
         Dictionary<String, String> mappedObject = MapObject(updatedItem);
         String updateQuery = QueryBuilder.CreateUpdateQuery(TableName, mappedId, mappedObject);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(updateQuery, mappedId);
         if (affectedRows.Equals(0)) throw new RepositoryException("No user updated!\n");
      }

      public List<Trial> GetAll()
      {
         String getQuery = QueryBuilder.CreateGetAllQuery(TableName);
         Dictionary<String, String> arguments = new Dictionary<string, string>();
         return QueryBuilder.ExecuteReader<Trial>(getQuery, arguments, Converter);
      }

      public List<Participant> GetParticipantsForTrial(string trialName, AgeCategory ageCategory)
      {
         String query = "SELECT e.participantName, p.age, p.ageCategoryName, a.minAge, a.maxAge " +
                        "FROM entries e INNER JOIN participants p ON e.participantName=p.name INNER JOIN " +
                        "agecategories a ON p.ageCategoryName=a.categoryName " +
                        "WHERE e.trialName=@trialName AND p.ageCategoryName=@ageCategoryName";
         Dictionary<String, String> arguments = new Dictionary<string, string>
         {
            {"trialName", trialName},
            {"ageCategoryName", ageCategory.Name}
         };
         return QueryBuilder.ExecuteReader<Participant>(query, arguments, ConverterParticipant);
      }

      private Participant ConverterParticipant(IDataReader dataReader)
      {
         var name = dataReader.GetString(0);
         var age = dataReader.GetInt32(1);

         var idAgeCategory = dataReader.GetString(2);
         var minAge = dataReader.GetInt32(3);
         var maxAge = dataReader.GetInt32(4);

         var ageCategory = new AgeCategory(idAgeCategory, minAge, maxAge);
         return new Participant(name, age, ageCategory);
      }
      private Trial Converter(IDataReader dataReader)
      {
         var trialName = dataReader.GetString(0);
         var trialDifficulty = dataReader.GetInt32(1);
         return new Trial(trialName, trialDifficulty);
      }

      private Dictionary<String, String> MapObject(Trial item)
      {
         Dictionary<String, String> mappedObject = new Dictionary<string, string>();
         mappedObject.Add("trialName", item.Name);
         mappedObject.Add("trialDifficulty", item.Difficulty.ToString());
         return mappedObject;
      }

      private Dictionary<String, String> MapId(String id)
      {
         Dictionary<String, String> mappedObject = new Dictionary<string, string>();
         mappedObject.Add("trialName", id);
         return mappedObject;
      }
   }
}
