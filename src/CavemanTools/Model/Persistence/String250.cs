using System;

namespace CavemanTools.Model.Persistence
{
    public class String250:Tuple<string>
    {
        public String250(string item1) : base(item1)
        {
            item1.MustComplyWith(d=>d.Length<250,"Maximum length is 250");
        }

        public static implicit operator String250(string v) =>new String250(v);
        public static implicit operator string(String250 v) =>v.Item1;
        
    }
}