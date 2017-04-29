using System;
using ServerCSharp.Domain;

namespace ServerCSharp.Validator
{
   public class UserValidator : IValidator<User>
   {
      public void Validate(User entity)
      {
         String errors = "";
         if (!ValidateUsername(entity.UserName)) errors += "Username can't be empty!\n";
         if (!ValidatePassword(entity.Password)) errors += "Password can't be empty!\n";

         if(errors.Length != 0) throw new ValidatorException(errors);
      }


      private Boolean ValidateUsername(String userName)
      {
         return userName.Length != 0;
      }

      private Boolean ValidatePassword(String password)
      {
         return password.Length != 0;
      }
   }
}
