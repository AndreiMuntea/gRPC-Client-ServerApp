using System;

namespace ServerCSharp.Domain
{
   public class Trial : IHasId<String>
   {
      public String Name { get; set; }
      public Int32 Difficulty { get; set; }

      public Trial(string name, int difficulty)
      {
         Name = name;
         Difficulty = difficulty;
      }

      public Trial()
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
         return $"{nameof(Name)}: {Name}, {nameof(Difficulty)}: {Difficulty}";
      }

      protected bool Equals(Trial other)
      {
         return string.Equals(Name, other.Name) && Difficulty == other.Difficulty;
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != this.GetType()) return false;
         return Equals((Trial) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return ((Name?.GetHashCode() ?? 0) * 397) ^ Difficulty;
         }
      }

      public static bool operator ==(Trial left, Trial right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(Trial left, Trial right)
      {
         return !Equals(left, right);
      }
   }
}
