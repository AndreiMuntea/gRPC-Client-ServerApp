using System;
using System.Runtime.Serialization;
using ServerCSharp.Utils.Exceptions;

namespace ServerCSharp.Service
{
   public class ServiceException : CustomException
   {
      public ServiceException()
      {
      }

      public ServiceException(string message) : base(message)
      {
      }

      public ServiceException(string message, Exception innerException) : base(message, innerException)
      {
      }

      protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }

}
