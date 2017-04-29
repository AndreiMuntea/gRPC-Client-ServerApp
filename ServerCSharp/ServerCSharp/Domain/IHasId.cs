namespace ServerCSharp.Domain
{
   public interface IHasId<T>
   {
      void SetId(T id);
      T GetId();
   }
}
