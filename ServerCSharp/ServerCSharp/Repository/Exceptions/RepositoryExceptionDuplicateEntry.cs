using System;
using System.Runtime.Serialization;

namespace ServerCSharp.Repository.Exceptions
{
   class RepositoryExceptionDuplicateEntry : RepositoryException
   {
      public RepositoryExceptionDuplicateEntry()
      {
      }

      public RepositoryExceptionDuplicateEntry(string message) : base(message)
      {
      }

      public RepositoryExceptionDuplicateEntry(string message, Exception innerException) : base(message, innerException)
      {
      }

      protected RepositoryExceptionDuplicateEntry(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }
}
