using System;
using System.Collections.Generic;
using System.Linq;

namespace CavemanTools.Localization
{
    public class MultilingualTexts
    {
        class Item
        {
            public string Language;
            public string Key;
            public string Value;

            public Item(string language, string key, string value)
            {
                Language = language;
                Key = key;
                Value = value;
            }
        }
        List<Item> _items=new List<Item>();
        List<Translations> _translations=new List<Translations>();
        public void Add(Translations translations)
        {
            _translations.Add(translations);
            _items.AddRange(translations.Select(kv => new Item(translations.Language, kv.Key, kv.Value)));
        }

        public IEnumerable<KeyValuePair<string, string>> TranslationFor(string textId)
        {
            return _items.Where(t => t.Key== textId).Select(t => new KeyValuePair<string, string>(t.Key, t.Value));
        }

        public Translations GetTranslations(string language)
        {
            return _translations.Find(t => t.Language == language);
        }

        public IEnumerable<string> GetLanguageForValue(string value)
            => _items.Where(d => d.Value == value).Select(d=>d.Language);
    }
}