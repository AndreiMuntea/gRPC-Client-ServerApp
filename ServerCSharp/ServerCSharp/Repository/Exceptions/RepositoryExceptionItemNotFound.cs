using System;
using System.Runtime.Serialization;

namespace ServerCSharp.Repository.Exceptions
{
   class RepositoryExceptionItemNotFound : RepositoryException
   {
      public RepositoryExceptionItemNotFound()
      {
      }

      public RepositoryExceptionItemNotFound(string message) : base(message)
      {
      }

      public RepositoryExceptionItemNotFound(string message, Exception innerException) : base(message, innerException)
      {
      }

      protected RepositoryExceptionItemNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }
}
