using System;

namespace CavemanTools.Model.Persistence
{
    public class String500:Tuple<string>
    {
        public String500(string item1) : base(item1)
        {
            item1.MustComplyWith(d=>d.Length<=500,"Maximum length is 500");
        }

        public static implicit operator String500(string v) =>new String500(v);
        public static implicit operator string(String500 v) =>v.Item1;
        
    }
}