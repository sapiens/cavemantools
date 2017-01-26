using System;
using System.Collections.Generic;

namespace CavemanTools.Localization
{
    public class Translations:Dictionary<string,string>
    {
        public string Language { get; private set; }

        public Translations(string language)
        {
            language.MustNotBeNull();
            Language = language;
        }
    }
}