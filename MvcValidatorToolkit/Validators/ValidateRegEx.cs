using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace System.Web.Mvc
{
	public class ValidateRegEx : Validator
	{
		public string Expression { get; set; }

		public ValidateRegEx(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		public ValidateRegEx(string elementsToValidate, string expression)
			: base(elementsToValidate)
		{
			if(string.IsNullOrEmpty(expression))
				throw new ArgumentNullException("expression");

			Expression = expression;
		}

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				//if(!Values.ContainsKey(element) || !Values.ContainsKey(ReferenceElement) || (Values[element] ?? string.Empty) == (Values[ReferenceElement] ?? string.Empty))
				if(true)
				{
					InvalidElements.Add(element);

					string label = ValidationSet.GetLocalizedText(element);
					ErrorMessages.Add(element, string.Format(ErrorMessageFormat, (label != null ? label : element)));
				}
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value with specific format";
		}

		protected override string GetClientRule(string element)
		{
			return "regEx:'" + Expression + "'";
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			string label = ValidationSet.GetLocalizedText(element);
			return string.Format("regEx:'{0}'", string.Format(ErrorMessageFormat, (label != null ? label : element))).Replace("'", "\'");
		}
	}
}
