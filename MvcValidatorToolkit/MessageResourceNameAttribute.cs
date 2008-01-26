using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class MessageResourceNameAttribute : Attribute
	{
		public string ResourceName { get; set; }

		public MessageResourceNameAttribute(string resourceName)
		{
			if(string.IsNullOrEmpty(resourceName))
				throw new ArgumentNullException("resourceName");

			ResourceName = resourceName;
		}
	}
}
