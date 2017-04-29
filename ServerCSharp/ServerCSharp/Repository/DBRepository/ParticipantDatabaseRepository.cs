using System;
using System.Collections.Generic;
using System.Data;
using ServerCSharp.Domain;
using ServerCSharp.Repository.DBRepository.DBConnection;
using ServerCSharp.Repository.Exceptions;
using ServerCSharp.Repository.Interface;

namespace ServerCSharp.Repository.DBRepository
{
   public class ParticipantDatabaseRepository : IParticipantRepository
   {
      public String TableName { get; set; }
      public QueryBuilder QueryBuilder { get; set; }

      public ParticipantDatabaseRepository()
      {
      }

      public ParticipantDatabaseRepository(string tableName, QueryBuilder queryBuilder)
      {
         TableName = tableName;
         QueryBuilder = queryBuilder;
      }

      public void AddItem(Participant item)
      {
         if (ExistsItem(item.Name)) throw new RepositoryExceptionDuplicateEntry("Duplicate Entry for participant!\n");

         Dictionary<String, String> mappedObject = MapObject(item);
         String addQuery = QueryBuilder.CreateInsertQuery(TableName, mappedObject);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(addQuery, mappedObject);
         if (affectedRows.Equals(0)) throw new RepositoryException("No participant inserted!\n");
      }

      public void DeleteItem(string id)
      {
         if (!ExistsItem(id)) throw new RepositoryException("No participant found!\n");

         Dictionary<String, String> mappedId = MapId(id);
         String deleteQuery = QueryBuilder.CreateDeleteQuery(TableName, mappedId);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(deleteQuery, mappedId);
         if (affectedRows.Equals(0)) throw new RepositoryException("No participant deleted!\n");
      }

      public bool ExistsItem(string id)
      {
         Dictionary<String, String> mappedId = MapId(id);
         String getQuery = QueryBuilder.CreateExistsQuery(TableName, mappedId);
         Int32 result = QueryBuilder.ExecuteScalar<Int32>(getQuery, mappedId, Convert.ToInt32);
         return result != 0;
      }

      public Participant GetItem(string id)
      {
         if (!ExistsItem(id)) throw new RepositoryException("No participant found!\n");

         Dictionary<String, String> mappedId = MapId(id);
         String getQuery =
            "SELECT name,age,a.categoryName,minAge,maxAge FROM participants p INNER JOIN ageCategories a " +
            "ON p.ageCategoryName=a.categoryName WHERE name=@name";

         return QueryBuilder.ExecuteReader<Participant>(getQuery, mappedId, Converter)[0];
      }

      public void UpdateItem(Participant updatedItem)
      {
         if (!ExistsItem(updatedItem.Name)) throw new RepositoryException("No participant found!\n");

         Dictionary<String, String> mappedId = MapId(updatedItem.Name);
         Dictionary<String, String> mappedObject = MapObject(updatedItem);
         String updateQuery = QueryBuilder.CreateUpdateQuery(TableName, mappedId, mappedObject);
         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(updateQuery, mappedId);
         if (affectedRows.Equals(0)) throw new RepositoryException("No participant updated!\n");
      }

      public List<Participant> GetAll()
      {
         String getQuery =
            "SELECT name,age,a.categoryName,minAge,maxAge FROM participants p INNER JOIN agecategories a " +
            "ON p.ageCategoryName=a.categoryName";
         Dictionary<String, String> arguments = new Dictionary<string, string>();
         return QueryBuilder.ExecuteReader<Participant>(getQuery, arguments, Converter);
      }

      public int CountTrialsForParticipant(string participantName)
      {
         String countQuery = "COUNT(*) FROM participants WHERE name=@name";
         Dictionary<String, String> arguments = new Dictionary<string, string> {{"name", participantName}};
         return QueryBuilder.ExecuteScalar<Int32>(countQuery, arguments, Convert.ToInt32);
      }


      public void RegisterParticipant(Participant participant, string trialName)
      {
         String registerQuery = "INSERT INTO entries(participantName, trialName) VALUES(@participantName, @trialName)";
         Dictionary<String, String> arguments = new Dictionary<string, string>();
         arguments.Add("participantName", participant.Name);
         arguments.Add("trialName", trialName);

         Int32 affectedRows = QueryBuilder.ExecuteNonQuery(registerQuery, arguments);
         if (affectedRows.Equals(0)) throw new RepositoryException("No participant registered!\n");
      }

      public List<string> GetTrialsForParticipant(string participantName)
      {
         String selectQuery = "SELECT trialName FROM entries WHERE participantName=@participantName";
         Dictionary<String, String> arguments = new Dictionary<string, string> {{ "participantName", participantName}};
         return QueryBuilder.ExecuteReader<String>(selectQuery, arguments, ConvertTrialName);
      }

      private String ConvertTrialName(IDataReader dataReader)
      {
         return dataReader.GetString(0);
      }

      private Participant Converter(IDataReader dataReader)
      {
         var name = dataReader.GetString(0);
         var age = dataReader.GetInt32(1);

         var idAgeCategory = dataReader.GetString(2);
         var minAge = dataReader.GetInt32(3);
         var maxAge = dataReader.GetInt32(4);

         var ageCategory = new AgeCategory(idAgeCategory, minAge, maxAge);
         return new Participant(name, age, ageCategory);
      }

      private Dictionary<String, String> MapObject(Participant item)
      {
         Dictionary<String, String> mappedObject = new Dictionary<string, string>();
         mappedObject.Add("name", item.Name);
         mappedObject.Add("age", item.Age.ToString());
         mappedObject.Add("ageCategoryName", item.AgeCategory.Name);
         return mappedObject;
      }

      private Dictionary<String, String> MapId(String id)
      {
         Dictionary<String, String> mappedObject = new Dictionary<string, string>();
         mappedObject.Add("name", id);
         return mappedObject;
      }
   }
}