using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public class UniqueValue
    {
        public const string DefaultAspect = "name";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Value that needs to be unique. An aspect has only one value</param>
        /// <param name="aspect">What aspect of the concept the value refers to. Eg: name, email</param>
        /// <param name="scope">Scope where the value should be unique</param>
        public UniqueValue(string value,string aspect= DefaultAspect, string scope="[none]")
        {
            aspect.MustNotBeEmpty();
            value.MustNotBeEmpty();
            scope.MustNotBeEmpty();
            Scope = scope;
            Value = value;
            Aspect = aspect;
        }

        public string Aspect { get;  }
        public string Scope { get;  }
        public string Value { get; }
    }
}