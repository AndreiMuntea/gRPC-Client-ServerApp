using System.Runtime.Serialization;

namespace ServerCSharp.Utils.Exceptions
{
   public class CustomException : System.Exception
   {
      public CustomException()
      {
      }

      public CustomException(string message) : base(message)
      {
      }

      public CustomException(string message, System.Exception innerException) : base(message, innerException)
      {
      }

      protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }
}
