using System;
using System.Collections.Generic;
using System.Linq;

namespace CavemanTools
{
    public class MultilingualTexts
    {
        List<Tuple<string,string,string>> _items=new List<Tuple<string, string, string>>();
        List<Translations> _translations=new List<Translations>();
        public void Add(Translations translations)
        {
            _translations.Add(translations);
            _items.AddRange(translations.Select(kv => new Tuple<string, string, string>(translations.Language, kv.Key, kv.Value)));
        }

        public IEnumerable<KeyValuePair<string, string>> TranslationFor(string textId)
        {
            return _items.Where(t => t.Item2 == textId).Select(t => new KeyValuePair<string, string>(t.Item1, t.Item3));
        }

        public Translations GetTranslations(string language)
        {
            return _translations.Find(t => t.Language == language);
        }
    }
}