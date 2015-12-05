using System;

namespace CavemanTools.Model.Persistence
{
    public class String75:Tuple<string>
    {
        public String75(string item1) : base(item1)
        {
            item1.MustComplyWith(d=>d.Length<=75,"Maximum length is 75");
        }

        public static implicit operator String75(string v) =>new String75(v);
        public static implicit operator string(String75 v) =>v.Item1;
        
    }
}