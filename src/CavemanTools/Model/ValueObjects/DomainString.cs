using System;
using System.Text.RegularExpressions;

namespace CavemanTools.Model.ValueObjects
{
    public abstract record DomainString :IEquatable<string>
    {
        public const string BaseFormat=@"^[a-zA-Z]+[\w\. \d\-_]*?$";

        protected DomainString(string value)
        {
			Value = value;
		}

		public string Value { get; }

		protected static bool IsValid(string value, int minLength=1, int maxLength=75, string format = BaseFormat)
        {
            var result = !value.IsNullOrEmpty(true) && value.Length >= minLength && value.Length<=maxLength;
            if (!result) return false;
            return format.IsNullOrEmpty() || Regex.IsMatch(value, format);
        }

        public bool Equals(string other) => other != null && Value == other;
		
	}

   
}