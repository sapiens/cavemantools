using System;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace CavemanTools.Xml
{
	internal class XmlToObject<T> where T:new()
	{
		private Type _type;

		internal XmlToObject()
		{
			Object= new T();
			_type = typeof (T);
		}

		internal void Parse(XmlNode node)
		{
			if (TryField(node)) return;
			TryProperty(node);
		}

		bool TryField(XmlNode node)
		{
			var f = _type.GetField(node.Name, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public);
			if (f == null || Ignore(f)) return false;

			if (f.FieldType.IsEnum)
			{
				var st = f.FieldType;
				f.SetValue(Object, Enum.Parse(st, node.InnerText));
				return true;
			}

			if (f.FieldType.Equals(typeof(TimeSpan)))
			{
				var tc = new TimeSpanConverter();

				f.SetValue(Object, (TimeSpan?)tc.ConvertFromString(node.InnerText));
				return true;
			}
			
			f.SetValue(Object, Convert.ChangeType(node.InnerText, f.FieldType));
			return true;
		}

		bool Ignore(MemberInfo info)
		{
			var ignore = info.GetCustomAttributes(typeof(XmlIgnoreAttribute), false);
			return ignore.Length > 0;
		}

		void TryProperty(XmlNode attr)
		{
			var f = _type.GetProperty(attr.Name, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public);
			if (f == null || Ignore(f)) return;
			 
			if (f.PropertyType.IsEnum)
			{
				var st = f.PropertyType;
				f.SetValue(Object, Enum.Parse(st, attr.InnerText), null);
				return;
			}

			if (f.PropertyType.Equals(typeof(TimeSpan?)))
			{
				var tc = new TimeSpanConverter();

				f.SetValue(Object, (TimeSpan?)tc.ConvertFromString(attr.InnerText),null);
				return;
			}

			if (f.PropertyType.Equals(typeof(TimeSpan)))
			{
				 
				f.SetValue(Object, TimeSpan.Parse(attr.InnerText), null);
				return;
			}

			f.SetValue(Object, Convert.ChangeType(attr.InnerText, f.PropertyType), null);	
		}

		internal T Object
		{
			get; private set;
		}
	}
}