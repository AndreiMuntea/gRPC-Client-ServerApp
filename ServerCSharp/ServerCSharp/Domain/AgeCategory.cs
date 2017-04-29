using System;

namespace ServerCSharp.Domain
{
   public class AgeCategory : IHasId<String>
   {
      public String Name { get; set; }
      public Int32 MinAge { get; set; }
      public Int32 MaxAge { get; set; }
      public AgeCategory()
      {
      }

      public AgeCategory(String name, int minAge, int maxAge)
      {
         Name = name;
         MinAge = minAge;
         MaxAge = maxAge;
      }

      public void SetId(String name)
      {
         Name = name;
      }

      public String GetId()
      {
         return Name;
      }

      public override string ToString()
      {
         return $"{nameof(Name)}: {Name}, {nameof(MinAge)}: {MinAge}, {nameof(MaxAge)}: {MaxAge}";
      }

      protected bool Equals(AgeCategory other)
      {
         return Name == other.Name && MinAge == other.MinAge && MaxAge == other.MaxAge;
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != this.GetType()) return false;
         return Equals((AgeCategory) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ MinAge;
            hashCode = (hashCode * 397) ^ MaxAge;
            return hashCode;
         }
      }

      public static bool operator ==(AgeCategory left, AgeCategory right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(AgeCategory left, AgeCategory right)
      {
         return !Equals(left, right);
      }
   }
}

