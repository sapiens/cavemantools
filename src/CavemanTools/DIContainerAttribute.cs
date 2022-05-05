using System;

namespace CavemanTools
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DIContainerAttribute : Attribute
	{
        public bool IsSingleton { get; set; } = false;
        /// <summary>
        /// True if you want the service to be context (usualy per request) scoped
        /// </summary>
        public bool IsScoped { get; set; } = false;

        public string Custom { get; set; } = "";
		
		 /// <summary>
        /// If true, the DI Container should ignore this class
        /// </summary>
		public bool Ignore {get;set;}=false;
	}
}