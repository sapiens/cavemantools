using System;
using System.Text.RegularExpressions;

namespace CavemanTools.Model.ValueObjects
{
    public abstract class DomainString : AbstractValueObject<string>,IEquatable<string>
    {
        public const string BaseFormat=@"^[a-zA-Z]+[\w\. \d\-_]*?$";

        protected DomainString(string value):base(value)
        {
            
        }

        protected static bool IsValid(string value, int minLength=1, int maxLength=75, string format = BaseFormat)
        {
            var result = !value.IsNullOrEmpty(true) && value.Length >= minLength && value.Length<=maxLength;
            if (!result) return false;
            return format.IsNullOrEmpty() || Regex.IsMatch(value, format);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(string other)
        {
            return other != null && other == _value;
        }

        public override string ToString()
        {
            return _value;
        }
    }

   
}