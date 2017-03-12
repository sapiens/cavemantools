using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public class UniqueValue
    {
        public const string DefaultAspect = "name";
        private const string DefaultScope = "[none]";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Value that needs to be unique. An aspect has only one value</param>
        /// <param name="aspect">What aspect of the concept the value refers to. Eg: name, email</param>
        /// <param name="scope">Scope where the value should be unique</param>
        public UniqueValue(string value,string aspect= DefaultAspect, string scope= DefaultScope)
        {
            aspect.MustNotBeEmpty();
            value.MustNotBeEmpty();
            scope.MustNotBeEmpty();
            Scope = scope;
            Value = value;
            Aspect = aspect;
        }

        /// <summary>
        /// Gets aspect of the data the value refers to. Eg: name, email
        /// </summary>
        public string Aspect { get;  }
        /// <summary>
        /// Gets scope/context where the value should be unique
        /// </summary>
        public string Scope { get;  }
        public string Value { get; }
    }
}