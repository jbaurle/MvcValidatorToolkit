using System;

namespace System.Web.Mvc
{
	public class ValidatorException : Exception
	{
		public string InvalidElement;

		public ValidatorException(string invalidElement, string message)
			: base(message)
		{
			InvalidElement = invalidElement;
		}

		public ValidatorException(string invalidElement, string message, Exception innerException)
			: base(message, innerException)
		{
			InvalidElement = invalidElement;
		}
	}
}
