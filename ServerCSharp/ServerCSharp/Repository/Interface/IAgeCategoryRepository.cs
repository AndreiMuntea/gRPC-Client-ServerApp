using System;
using ServerCSharp.Domain;

namespace ServerCSharp.Repository.Interface
{
   public interface IAgeCategoryRepository : ICrudRepository<String, AgeCategory>
   {
      AgeCategory FindSuitableAgeCategory(Int32 age);
      AgeCategory FindSuitableAgeCategory(Int32 minAge, Int32 maxAge);
   }
}
