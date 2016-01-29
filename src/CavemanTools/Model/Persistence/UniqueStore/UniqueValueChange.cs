using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public class UniqueValueChange
    {
        public UniqueValueChange(string value, string aspect=UniqueValue.DefaultAspect)
        {
            value.MustNotBeEmpty();
           
            aspect.MustNotBeEmpty();
            Value = value;
         
            Aspect = aspect;
        }

        public string Value { get; }   
        public string Aspect { get;}
    }
}