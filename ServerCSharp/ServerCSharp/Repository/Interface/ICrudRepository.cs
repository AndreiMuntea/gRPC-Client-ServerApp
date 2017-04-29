using System;
using System.Collections.Generic;
using ServerCSharp.Domain;

namespace ServerCSharp.Repository.Interface
{
   public interface ICrudRepository<TId, T> where T:IHasId<TId>
   {
      void AddItem(T item);
      void DeleteItem(TId id);
      Boolean ExistsItem(TId id);
      T GetItem(TId id);
      void UpdateItem(T updatedItem);
      List<T> GetAll();
   }
}
