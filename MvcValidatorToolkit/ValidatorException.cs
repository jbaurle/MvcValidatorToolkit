using System;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validation error that occur during the process of validation.
	/// </summary>
	public class ValidatorException : Exception
	{
		public string InvalidElement;

		/// <summary>
		/// Initializes a new instance of the ValidatorException class with the given element and
		/// the error message.
		/// </summary>
		public ValidatorException(string invalidElement, string message)
			: base(message)
		{
			InvalidElement = invalidElement;
		}
	}
}
