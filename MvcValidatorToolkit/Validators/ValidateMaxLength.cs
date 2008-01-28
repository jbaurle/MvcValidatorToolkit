using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	public class ValidateMaxLength : Validator
	{
		public int MaxLength { get; set; }

		public ValidateMaxLength(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		public ValidateMaxLength(string elementsToValidate, int maxLength)
			: base(elementsToValidate)
		{
			if(maxLength < 0)
				throw new ArgumentOutOfRangeException("maxLength");

			MaxLength = maxLength;
		}

		public override string GetClientRule(string element)
		{
			return "maxLength:" + MaxLength;
		}

		public override string GetClientMessage(string element)
		{
			return string.Format("maxLength:'{0}'", GetLocalizedErrorMessage(element, MaxLength)).Replace("'", "\'");
		}

		protected override void Validate(string element)
		{
			if(Values.ContainsKey(element) == false || (Values[element] ?? string.Empty).Trim().Length > MaxLength)
				InsertError(element, MaxLength);
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value value no longer than {1} characters";
		}
	}
}
