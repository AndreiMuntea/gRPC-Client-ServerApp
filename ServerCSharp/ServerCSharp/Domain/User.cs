using System;

namespace ServerCSharp.Domain
{
   public class User : IHasId<String>
   {
      public String UserName { get; set; }
      public String Password { get; set; }
      public User()
      {
      }

      public User(string userName, string password)
      {
         UserName = userName;
         Password = password;
      }

      public void SetId(string id)
      {
         UserName = id;
      }

      public string GetId()
      {
         return UserName;
      }

      public override string ToString()
      {
         return $"{nameof(UserName)}: {UserName}, {nameof(Password)}: {Password}";
      }

      protected bool Equals(User other)
      {
         return string.Equals(UserName, other.UserName) && string.Equals(Password, other.Password);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != this.GetType()) return false;
         return Equals((User) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return ((UserName?.GetHashCode() ?? 0) * 397) ^ (Password?.GetHashCode() ?? 0);
         }
      }

      public static bool operator ==(User left, User right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(User left, User right)
      {
         return !Equals(left, right);
      }
   }
}
