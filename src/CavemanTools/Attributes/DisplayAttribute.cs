using System;
using CavemanTools.Reflection;

namespace CavemanTools.Attributes
{
	public class DisplayAttribute:Attribute
	{
		public string Name { get; set; }

		public string ResourceName { get; set; }
		public Type ResourceType { get; set; }

		public override string ToString()
		{
			if (!string.IsNullOrEmpty(Name)) return Name;
			if (!string.IsNullOrEmpty(ResourceName))
			{
				if (ResourceType == null) throw new NullReferenceException("ResourceType has to be specified");
				return ResourceType.GetStaticProperty(ResourceName);
			}
			return string.Empty;
		}
	}
}