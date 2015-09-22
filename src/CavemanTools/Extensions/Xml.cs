using System.Collections.Generic;
using CavemanTools.Xml;

namespace System.Xml
{
	public static class Xml
	{
		/// <summary>
		/// Returns true if an xml node is a comment
		/// </summary>
		/// <param name="node">XmlNode</param>
		/// <returns></returns>
		public static bool IsComment(this XmlNode node)
		{
			if (node == null) throw new ArgumentNullException("node");
			return (node.NodeType == XmlNodeType.Comment);
		}
		
		/// <summary>
		/// Returns true if the node has an attribute of specified name
		/// </summary>
		/// <param name="node">xmlNode</param>
		/// <param name="name">Attribute name</param>
		/// <returns></returns>
		public static bool HasAttribute(this XmlNode node, string name)
		{
			if (node == null) throw new ArgumentNullException("node");
			if (name == null) throw new ArgumentNullException("name");
			return node.Attributes[name] != null;
		}


		/// <summary>
		/// Gets the attribute value. If it doesn't exist it returns the default of the type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="node"></param>
		/// <param name="name">Name of attribute</param>
		/// <returns></returns>
		public static T GetAttributeValue<T>(this XmlNode node, string name)
		{
			return GetAttributeValue(node, name, default(T));
		}

		/// <summary>
		/// Gets the attribute value. If it doesn't exist it returns the provided value
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="node"></param>
		/// <param name="name">Name of attribute</param>
		/// <param name="defaultValue">Value to return if attribute does not exists</param>
		/// <returns></returns>
		public static T GetAttributeValue<T>(this XmlNode node, string name, T defaultValue)
		{
			if (node == null)
			{
				return defaultValue;
			}
			if (node.Attributes[name] == null)
			{
				return defaultValue; 
			}
			return node.Attributes[name].GetValue<T>();
		}

		
		/// <summary>
		/// Gets value of xml as type.
		/// Returns default of type if not existant or empty.
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		public static T GetValue<T>(this XmlNode node)
		{
			if (node==null) return default(T);
			return node.InnerText.ConvertTo<T>();			
		}


		/// <summary>
		/// Gets value of xml as type.
		/// Returns provided value if null.
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="node"></param>
		/// <param name="defaultValue">value if node is null</param>
		/// <returns></returns>
		public static T GetValue<T>(this XmlNode node, T defaultValue)
		{
			if (node == null) return defaultValue;
			return node.InnerText.ConvertTo<T>();
		}

	
		#region Read object from xml elements
		/// <summary>
		/// Converts xmlnode to object whose properties are the attributes names. 
		/// </summary>
		/// <typeparam name="T">Type with parameterless constructor</typeparam>
		/// <param name="el"></param>
		/// <returns></returns>
		public static T AttributesToObject<T>(this XmlNode el) where T : new()
		{
			if (el == null || el.Attributes==null) return default(T);
			var x = new XmlToObject<T>();
			foreach (XmlAttribute attr in el.Attributes)
			{
				x.Parse(attr);
			}
			return x.Object;
		}

		/// <summary>
		/// Converts xmlnode to object whose properties are the xml children names. 
		/// </summary>
		/// <typeparam name="T">Type with parameterless constructor</typeparam>
		/// <param name="el"></param>
		/// <returns></returns>
		public static T ChildrenToObject<T>(this XmlNode el) where T : new()
		{
			if (el == null) return default(T);
			var r = new XmlToObject<T>();
			foreach (XmlNode x in el.ChildNodes)
			{
				r.Parse(x);
			}
			return r.Object;
		}

		/// <summary>
		/// Converts xmlnode to Dictionary whose keys are the xml children names and values are children's inner text. 
		/// </summary>
		/// <typeparam name="T">Type with parameterless constructor</typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		public static IDictionary<T, V> ChildrenToDictionary<T, V>(this XmlNode node)
		{
			var dt = new Dictionary<T, V>();
			if (node == null) return dt;
			foreach (XmlNode c in node)
			{
				if (string.IsNullOrEmpty(c.InnerText))
				{
					dt.Add(c.Name.ConvertTo<T>(), default(V));
					continue;
				}
				dt.Add(c.Name.ConvertTo<T>(), c.InnerText.ConvertTo<V>());
			}
			return dt;
		}

		/// <summary>
		/// Converts xmlnode to Dictionary whose keys are the attributes names and values are attributes' inner text. 
		/// </summary>
		/// <typeparam name="T">Type with parameterless constructor</typeparam>
		/// <param name="node"></param>
		/// <returns></returns>
		public static IDictionary<T, V> AttributesToDictionary<T, V>(this XmlNode node)
		{
			var dt = new Dictionary<T, V>();
			if (node == null || node.Attributes==null) return dt;
			foreach (XmlNode c in node.Attributes)
			{
				if (string.IsNullOrEmpty(c.InnerText))
				{
					dt.Add(c.Name.ConvertTo<T>(), default(V));
					continue;
				}
				dt.Add(c.Name.ConvertTo<T>(), c.InnerText.ConvertTo<V>());
			}
			return dt;
		}
		#endregion


		
	}
}