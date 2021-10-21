using System;

namespace CavemanTools.Model.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public record Email(string Value)
    {
        
        public static Email CreateRandomTestValue()
        {
            return new Email("test"+Guid.NewGuid().ToString().Substring(4)+"@exmple.com");
        }
                 
        public static bool IsValid(string value)
        {
            return value.IsEmail();
        }
    }
}