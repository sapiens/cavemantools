using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public class UniqueValueChange
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="aspect"> Aspect of the data the value refers to. Eg: name, email</param>
        public UniqueValueChange(string value, string aspect=UniqueValue.DefaultAspect)
        {
            value.MustNotBeEmpty();
           
            aspect.MustNotBeEmpty();
            Value = value;
         
            Aspect = aspect;
        }


        public string Value { get; }
        /// <summary>
        ///  Gets aspect of the data the value refers to. Eg: name, email
        /// </summary>
        public string Aspect { get;}
    }
}