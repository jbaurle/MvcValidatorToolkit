using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	public class ValidatePresence : Validator
	{
		public ValidatePresence(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		public override string GetClientRule(string element)
		{
			return "required:true";
		}

		public override string GetClientMessage(string element)
		{
			return string.Format("required:'{0}'", GetLocalizedErrorMessage(element)).Replace("'", "\'");
		}

		protected override void Validate(string element)
		{
			if(Values.ContainsKey(element) == false || (Values[element] ?? string.Empty).Trim().Length == 0)
				InsertError(element);
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} is required";
		}
	}
}
