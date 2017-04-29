using System;
using System.Collections.Generic;
using ServerCSharp.Domain;
using ServerCSharp.Repository.Interface;
using ServerCSharp.Validator;

namespace ServerCSharp.Service
{
   public class AgeCategoryService
   {
      private readonly IAgeCategoryRepository _repository;
      private readonly IValidator<AgeCategory> _validator;

      public AgeCategoryService(IAgeCategoryRepository repository, IValidator<AgeCategory> validator)
      {
         _repository = repository;
         _validator = validator;
      }

      public void AddAgeCategory(String ageCategoryName, String minAge, String maxAge)
      {
         Int32 minAgeArgument = Converter.ConvertToInt32(minAge);
         Int32 maxAgeArgument = Converter.ConvertToInt32(maxAge);

         AgeCategory ageCategory = new AgeCategory(ageCategoryName, minAgeArgument, maxAgeArgument);
         _validator.Validate(ageCategory);
         _repository.AddItem(ageCategory);

      }

      public void DeleteAgeCategory(String ageCategoryName)
      {
         _repository.DeleteItem(ageCategoryName);
      }

      public void UpdateAgeCategory(String ageCategoryName, String minAge, String maxAge)
      {
         Int32 minAgeArgument = Converter.ConvertToInt32(minAge);
         Int32 maxAgeArgument = Converter.ConvertToInt32(maxAge);

         AgeCategory ageCategory = new AgeCategory(ageCategoryName, minAgeArgument, maxAgeArgument);
         _validator.Validate(ageCategory);
         _repository.UpdateItem(ageCategory);
      }

      public AgeCategory GetAgeCategory(String ageCategoryName)
      {
         return _repository.GetItem(ageCategoryName);
      }

      public Boolean ExistsItem(String ageCategoryName)
      {
         return _repository.ExistsItem(ageCategoryName);
      }

      public List<AgeCategory> GetAll()
      {
         return _repository.GetAll();
      }

      public AgeCategory FindSuitableAgeCategory(String age)
      {
         Int32 ageArgument = Converter.ConvertToInt32(age);
         return _repository.FindSuitableAgeCategory(ageArgument);
      }

      public AgeCategory FindSuitableAgeCategory(String minAge, String maxAge)
      {
         Int32 minAgeArgument = Converter.ConvertToInt32(minAge);
         Int32 maxAgeArgument = Converter.ConvertToInt32(maxAge);
         return _repository.FindSuitableAgeCategory(minAgeArgument, maxAgeArgument);
      }

   }
}