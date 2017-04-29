using System;
using ServerCSharp.Domain;

namespace ServerCSharp.Repository.Interface
{
   public interface IUserRepository : ICrudRepository<String, User>
   {
      bool LogIn(User user);
   }
}
