namespace ServerCSharp.Validator
{
   public interface IValidator<T>
   {
      void Validate(T entity);
   }
}
