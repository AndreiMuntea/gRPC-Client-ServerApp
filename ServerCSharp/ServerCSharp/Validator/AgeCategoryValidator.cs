using System;
using ServerCSharp.Domain;

namespace ServerCSharp.Validator
{
   public class AgeCategoryValidator : IValidator<AgeCategory>
   {
      public void Validate(AgeCategory entity)
      {
         String errors = "";
         if (!ValidateAge(entity.MinAge, entity.MaxAge))
            errors += "Min age should be smaller than Max age!\n" +
                      "Min age should be greater than 0!\n" +
                      "Max age should be greater than 0!\n";
         if (!ValidateId(entity.Name)) errors += "Name shouldn't be empty!\n";

         if(errors.Length!=0) throw new ValidatorException(errors);
      }

      private Boolean ValidateId(String id)
      {
         return id.Length > 0;
      }

      private Boolean ValidateAge(Int32 minAge, Int32 maxAge)
      {
         return (minAge > 0 && maxAge > 0 && minAge < maxAge);
      }
   }
}
