using System;
using System.Collections.ObjectModel;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a client-side validator definition used in conjunction with the jQuery
	/// validation plugin.
	/// </summary>
	public class ValidatorMethodData
	{
		public string Name { get; set; }
		public string Function { get; set; }
		public string ErrorMessage { get; set; }

		/// <summary>
		/// Initializes a new instance of the ValidatorException class.
		/// </summary>
		public ValidatorMethodData()
		{
		}

		/// <summary>
		/// Initializes a new instance of the ValidatorException class.
		/// </summary>
		public ValidatorMethodData(string name, string function, string errorMessage)
		{
			Name = name;
			Function = function;
			ErrorMessage = errorMessage;
		}
	}
}
