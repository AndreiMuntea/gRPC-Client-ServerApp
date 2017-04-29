using System;

namespace ServerCSharp.Domain
{
   public class Participant : IHasId<String>
   {
      public String Name { get; set; }
      public Int32 Age { get; set; }
      public AgeCategory AgeCategory { get; set; }

      public Participant(string name, int age, AgeCategory ageCategory)
      {
         Name = name;
         Age = age;
         AgeCategory = ageCategory;
      }

      public Participant()
      {
      }

      public void SetId(string id)
      {
         Name = id;
      }

      public string GetId()
      {
         return Name;
      }

      public override string ToString()
      {
         return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}, {nameof(AgeCategory)}: {AgeCategory}";
      }

      protected bool Equals(Participant other)
      {
         return string.Equals(Name, other.Name) && Age == other.Age && Equals(AgeCategory, other.AgeCategory);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != this.GetType()) return false;
         return Equals((Participant) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = Name?.GetHashCode() ?? 0;
            hashCode = (hashCode * 397) ^ Age;
            hashCode = (hashCode * 397) ^ (AgeCategory != null ? AgeCategory.GetHashCode() : 0);
            return hashCode;
         }
      }

      public static bool operator ==(Participant left, Participant right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(Participant left, Participant right)
      {
         return !Equals(left, right);
      }
   }
}
