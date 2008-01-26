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

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				if(!Values.ContainsKey(element) || (Values[element] ?? string.Empty).Trim().Length < MinLength)
				{
					InvalidElements.Add(element);

					string label = ValidationSet.GetLocalizedText(element);
					ErrorMessages.Add(element, string.Format(ErrorMessageFormat, (label != null ? label : element), MinLength));
				}
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value of at least {1} characters";
		}

		protected override string GetClientRule(string element)
		{
			return "minLength:" + MinLength;
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			string label = ValidationSet.GetLocalizedText(element);
			return string.Format("minLength:'{0}'", string.Format(ErrorMessageFormat, (label != null ? label : element), MinLength)).Replace("'", "\'");
		}
	}
}
