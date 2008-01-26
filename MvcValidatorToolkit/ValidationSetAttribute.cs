using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Web.UI;

namespace System.Web.Mvc
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public class ValidationSetAttribute : Attribute
	{
		public ValidationSet ValidationSet { get; private set; }

		public ValidationSetAttribute(Type validationSetType)
		{
			if(!typeof(ValidationSet).IsAssignableFrom(validationSetType))
				throw new ArgumentException("The parameter validationSetType must derive from the ValidationSet class");

			ValidationSet = (ValidationSet)Activator.CreateInstance(validationSetType);
		}
	}
}
