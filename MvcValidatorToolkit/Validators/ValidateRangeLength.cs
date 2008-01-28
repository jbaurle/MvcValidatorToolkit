using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	public class ValidateRangeLength : Validator
	{
		public int MinLength { get; set; }
		public int MaxLength { get; set; }

		public ValidateRangeLength(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		public ValidateRangeLength(string elementsToValidate, int minLength, int maxLength)
			: base(elementsToValidate)
		{
			if(maxLength < 0)
				throw new ArgumentOutOfRangeException("maxLength");
			if(minLength < 0 || minLength > maxLength)
				throw new ArgumentOutOfRangeException("minLength");

			MinLength = minLength;
			MaxLength = maxLength;
		}

		public override string GetClientRule(string element)
		{
			return "rangeLength:[" + MinLength + "," + MaxLength + "]";
		}

		public override string GetClientMessage(string element)
		{
			return string.Format("rangeLength:'{0}'", GetLocalizedErrorMessage(element, MinLength, MaxLength)).Replace("'", "\'");
		}

		protected override void Validate(string element)
		{
			int length = (Values[element] ?? string.Empty).Trim().Length;

			if(Values.ContainsKey(element) == false || length < MinLength || length > MaxLength)
				InsertError(element, MinLength, MaxLength);
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value between {1} and {2} characters long";
		}
	}
}
