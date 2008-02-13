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
	/// <summary>
	/// Represents an attribute class to set the name of the validation set.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class ValidationSetAttribute : Attribute
	{
		public ValidationSet ValidationSet { get; private set; }

		/// <summary>
		/// Initialzes a new instance of the ValidationSetAttribute class with the type
		/// of the assoicated validation set.
		/// </summary>
		public ValidationSetAttribute(Type validationSetType)
		{
			if(!typeof(ValidationSet).IsAssignableFrom(validationSetType))
				throw new ArgumentException("The parameter validationSetType must derive from the ValidationSet class");

			// Creates an instance of the validation set
			ValidationSet = (ValidationSet)Activator.CreateInstance(validationSetType);
		}
	}
}
