using System;

namespace CavemanTools.Model.ValueObjects
{
    /// <summary>
    /// Encapsulates the url friendly version of a string
    /// </summary>
    public record UrlSlug
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="value">Value must not be empty</param>
        public UrlSlug(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(value);
            Value = value.MakeSlug();
        }

        public string Value { get; private set; }

     

        public override string ToString()
        {
            return Value;
        }
    }
}