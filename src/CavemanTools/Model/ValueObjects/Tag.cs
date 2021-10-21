using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CavemanTools.Model.ValueObjects
{
    public record Tag : IEquatable<string>
    {
       
        /// <summary>
        /// Tag validation regex
        /// </summary>
        public static string TagFormat = @"^[a-zA-Z]+[\w\. \d\-_]*?$";

        /// <summary>
        /// Maximum length of a tag
        /// </summary>
        public static byte MaxLength = 100;

		public string Value { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		/// <exception cref="ArgumentException"></exception>
		/// <param name="value"></param>
		public Tag(string value)
        {
            IsValid(value).MustBe(true);
            Value = value;
		}

        /// <summary>
        /// Splits the value into tags, separated by ','.
        /// All invalid tag values are ignored
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<Tag> CreateFrom(string value)
        {
            if (string.IsNullOrEmpty(value)) return new Tag[0];
            return value.Split(',')
                .Select(v => v.Trim())
                .Where(IsValid)
                .Select(CreateFromValid);
        }

        public static bool IsValid(string value)
        {
            return value != null && value.Length <= MaxLength && Regex.IsMatch(value, TagFormat);
        }

        /// <summary>
        /// It doesn't validate the value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Tag CreateFromValid(string value)
        {
            return new Tag(){Value = value};
        }

        private Tag()
        {
            
        }

        public static string Flatten(IEnumerable<Tag> tags)
        {
            return string.Join(",", tags.Select(t => t.Value));
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
            return other != null && other == Value;
        }
        

        public override string ToString()
        {
            return Value;
        }
    }
}