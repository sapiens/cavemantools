using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CavemanTools
{
    //public class Enumeration
    //{
    //    public Enumeration(int id, string displayName)
    //    {
    //        Id = id;
    //        DisplayName = displayName;
    //    }
    //    /// <summary>
    //    /// Field name containing a specific value of the enumeration. e.g: for Currency.USD field name is "USD"
    //    /// </summary>
    //    internal string Value { get; set; }

    //    public int Id { get; private set; }
    //    public string DisplayName { get; private set; }

    //    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    //    {
    //        return typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public)
    //            .Where(d => d.FieldType.DerivesFrom<Enumeration>())
    //            .Select(d => d.GetValue(null)).Cast<T>();
    //    }
    //}
}
