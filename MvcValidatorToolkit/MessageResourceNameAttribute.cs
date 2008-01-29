using System;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents an attribute class to set the name of the resource file in the App_GlobalResources
	/// folder.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class MessageResourceNameAttribute : Attribute
	{
		public string ResourceName { get; set; }

		/// <summary>
		/// Initialzes a new instance of the MessageResourceNameAttribute class with the resource 
		/// file name.
		/// </summary>
		public MessageResourceNameAttribute(string resourceName)
		{
			if(string.IsNullOrEmpty(resourceName))
				throw new ArgumentNullException("resourceName");

			ResourceName = resourceName;
		}
	}
}
