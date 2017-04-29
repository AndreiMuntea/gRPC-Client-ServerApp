using System;
using System.Runtime.Serialization;
using ServerCSharp.Utils.Exceptions;

namespace ServerCSharp.Repository.Exceptions
{
   class RepositoryException : CustomException
   {
      public RepositoryException()
      {
      }

      public RepositoryException(string message) : base(message)
      {
      }

      public RepositoryException(string message, Exception innerException) : base(message, innerException)
      {
      }

      protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }
}
