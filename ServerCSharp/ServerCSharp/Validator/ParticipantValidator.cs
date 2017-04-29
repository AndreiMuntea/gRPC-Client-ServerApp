using System;
using ServerCSharp.Domain;

namespace ServerCSharp.Validator
{
   public class ParticipantValidator : IValidator<Participant>
   {
      public void Validate(Participant entity)
      {
         String errors = "";
         if (!ValidateAge(entity.Age)) errors += "Invalid value for age!\n";
         if (!ValidateName(entity.Name)) errors += "Name can't be empty!\n";

         if(errors.Length != 0) throw new ValidatorException(errors);
      }

      private Boolean ValidateAge(Int32 age)
      {
         return (age > 0 && age < 150);
      }

      private Boolean ValidateName(String name)
      {
         return name.Length != 0;
      }
   }
}
