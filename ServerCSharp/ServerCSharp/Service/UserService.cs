using System.Collections.Generic;
using ServerCSharp.Domain;
using ServerCSharp.Repository.Interface;
using ServerCSharp.Validator;

namespace ServerCSharp.Service
{
   public class UserService
   {
      private readonly IUserRepository _repository;
      private readonly IValidator<User> _validator;

      public UserService(IUserRepository repository, IValidator<User> validator)
      {
         _repository = repository;
         _validator = validator;
      }

      public bool LogIn(string userName, string userPassword)
      {
         return _repository.LogIn(new User(userName, userPassword));
      }

      public List<User> GetAll()
      {
         return _repository.GetAll();
      }


   }
}
