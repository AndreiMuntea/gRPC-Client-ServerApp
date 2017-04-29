using System;
using System.Runtime.Serialization;
using ServerCSharp.Utils.Exceptions;

namespace ServerCSharp.Validator
{
   public class ValidatorException : CustomException
   {
      public ValidatorException()
      {
      }

      public ValidatorException(string message) : base(message)
      {
      }

      public ValidatorException(string message, Exception innerException) : base(message, innerException)
      {
      }

      protected ValidatorException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }
}
