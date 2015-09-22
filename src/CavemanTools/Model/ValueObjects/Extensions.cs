using System.Collections.Generic;

namespace CavemanTools.Model.ValueObjects
{
    public static class Extensions
    {
        public static IEnumerable<Tag> ToTags(this string data)
        {
            return Tag.CreateFrom(data);
        }
    }
}