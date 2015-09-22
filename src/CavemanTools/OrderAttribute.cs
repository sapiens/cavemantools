using System;

namespace CavemanTools
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method|AttributeTargets.Property)]
    public class OrderAttribute:Attribute
    {
        public OrderAttribute(int value)
        {
            Value = value;
        }
        public int Value { get; private set; }
        public string Group { get; set; }
    
    }
}