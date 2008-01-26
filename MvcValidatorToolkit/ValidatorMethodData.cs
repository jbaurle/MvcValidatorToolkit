using System;
using System.Collections.ObjectModel;

namespace System.Web.Mvc
{
	public class ValidatorMethodData
	{
		public string Name { get; set; }
		public string Function { get; set; }
		public string ErrorMessage { get; set; }

		public ValidatorMethodData()
		{
		}

		public ValidatorMethodData(string name, string function, string errorMessage)
		{
			Name = name;
			Function = function;
			ErrorMessage = errorMessage;
		}
	}
}
