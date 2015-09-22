using System.Collections.Generic;

namespace CavemanTools
{
    public class Translations:Dictionary<string,string>
    {
        public string Language { get; private set; }

        public Translations(string language)
        {
            Language = language;
        }
    }
}