using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	/*
	public class ValidateCompareTo : Validator
	{
		public string ReferenceElement { get; set; }

		public ValidateCompareTo(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		public ValidateCompareTo(string elementsToValidate, string referenceElement)
			: base(elementsToValidate)
		{
			if(string.IsNullOrEmpty(referenceElement))
				throw new ArgumentOutOfRangeException("referenceElement");

			ReferenceElement = referenceElement;
		}

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				if(!Values.ContainsKey(element) || !Values.ContainsKey(ReferenceElement) || (Values[element] ?? string.Empty) == (Values[ReferenceElement] ?? string.Empty))
				{
					InvalidElements.Add(element);

					string label = ValidationSet.GetLocalizedText(element);
					string referenceLabel = ValidationSet.GetLocalizedText(ReferenceElement);
					ErrorMessages.Add(element, string.Format(ErrorMessageFormat, (label != null ? label : element), (referenceLabel != null ? referenceLabel : ReferenceElement)));
				}
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain the same value as the field {1}";
		}

		protected override string GetClientRule(string element)
		{
			return "equalTo:'" + ReferenceElement + "'";
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			string label = ValidationSet.GetLocalizedText(element);
			string referenceLabel = ValidationSet.GetLocalizedText(ReferenceElement);
			return string.Format("equalTo:'{0}'", string.Format(ErrorMessageFormat, (label != null ? label : element), (referenceLabel != null ? referenceLabel : ReferenceElement))).Replace("'", "\'");
		}
	}
	*/
}
