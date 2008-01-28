using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	public class ValidateMinLength : Validator
	{
		public int MinLength { get; set; }

		public ValidateMinLength(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		public ValidateMinLength(string elementsToValidate, int minLength)
			: base(elementsToValidate)
		{
			if(minLength < 0)
				throw new ArgumentOutOfRangeException("minLength");

			MinLength = minLength;
		}

		public override string GetClientRule(string element)
		{
			return "minLength:" + MinLength;
		}

		public override string GetClientMessage(string element)
		{
			return string.Format("minLength:'{0}'", GetLocalizedErrorMessage(element, MinLength)).Replace("'", "\'");
		}

		protected override void Validate(string element)
		{
			if(Values.ContainsKey(element) == false || (Values[element] ?? string.Empty).Trim().Length < MinLength)
				InsertError(element, MinLength);
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value of at least {1} characters";
		}
	}
}
