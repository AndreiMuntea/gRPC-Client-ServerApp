using System;
using ServerCSharp.Domain;

namespace ServerCSharp.Validator
{
   public class TrialValidator : IValidator<Trial>
   {
      public void Validate(Trial entity)
      {
         String errors = "";
         if (!ValidateName(entity.Name)) errors += "Name can't be empty!\n";
         if (!ValidateDifficulty(entity.Difficulty)) errors += "Difficulty should be a positive integer!\n";

         if(errors.Length != 0) throw new ValidatorException(errors);
      }

      private Boolean ValidateName(String name)
      {
         return name.Length != 0;
      }

      private Boolean ValidateDifficulty(Int32 difficulty)
      {
         return difficulty > 0;
      }

   }
}
