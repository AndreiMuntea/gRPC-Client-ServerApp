using System;

namespace ServerCSharp.Service
{
   public static class Converter
   {
      public static Int32 ConvertToInt32(String value)
      {
         Int32 result;

         if(!Int32.TryParse(value,out result))
            throw new ServiceException("Can't convert value `"+value+"` to int!\n");

         return result;
      }
   }
}
