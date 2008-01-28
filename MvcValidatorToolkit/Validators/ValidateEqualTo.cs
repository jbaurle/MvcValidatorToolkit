using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	public class ValidateEqualTo : Validator
	{
		public string ReferenceElement { get; set; }

		public ValidateEqualTo(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		public ValidateEqualTo(string elementsToValidate, string referenceElement)
			: base(elementsToValidate)
		{
			if(string.IsNullOrEmpty(referenceElement))
				throw new ArgumentOutOfRangeException("referenceElement");

			ReferenceElement = referenceElement;
		}

		public override string GetClientRule(string element)
		{
			return "equalTo:'#" + ReferenceElement + "'";
		}

		public override string GetClientMessage(string element)
		{
			return string.Format("equalTo:'{0}'", GetLocalizedErrorMessage(element, ValidationSet.GetLocalizedText(ReferenceElement))).Replace("'", "\'");
		}

		protected override void Validate(string element)
		{
			if(Values.ContainsKey(element) == false || !Values.ContainsKey(ReferenceElement) || (Values[element] ?? string.Empty) != (Values[ReferenceElement] ?? string.Empty))
				InsertError(element, ValidationSet.GetLocalizedText(ReferenceElement));
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain the same value as the field {1}";
		}
	}
}
