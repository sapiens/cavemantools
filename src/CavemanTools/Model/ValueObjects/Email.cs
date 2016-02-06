using System;

namespace CavemanTools.Model.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class Email:AbstractValueObject<string>,IEquatable<Email>
    {
        
        public static Email CreateRandomTestValue()
        {
            return new Email("test"+Guid.NewGuid().ToString().Substring(4)+"@exmple.com");
        }
            

        public Email(string value) : base(value)
        {
            
        }

        protected override bool Validate(string value)
        {
            return IsValid(value);
        }

        public static bool IsValid(string value)
        {
            return value.IsEmail();
        }

        public bool Equals(Email other)
        {
            return other != null && other.Value == Value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Email);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}