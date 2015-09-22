using System;

namespace CavemanTools.Extensions
{
    public static class GuidExtensions
    {
         public static string ToBase64(this Guid id)
         {
             return Convert.ToBase64String(id.ToByteArray());
         }

        /// <summary>
        /// Expresses the Guid as 2 ulong numbers. Useful when you need to use the guid as a part of a number.
        /// Great for Azure tables rowkeys
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
 
        public static string AsNumberString(this Guid id)
         {
             var all = id.ToByteArray();
             var first = BitConverter.ToUInt64(all, 0);
             var second = BitConverter.ToUInt64(all, 8);
             return first + second.ToString();
         }
    }
}