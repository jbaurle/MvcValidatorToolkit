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

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				if(!Values.ContainsKey(element) || (Values[element] ?? string.Empty).Trim().Length > MaxLength)
				{
					InvalidElements.Add(element);

					string label = ValidationSet.GetLocalizedText(element);
					ErrorMessages.Add(element, string.Format(ErrorMessageFormat, (label != null ? label : element), MaxLength));
				}
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value value no longer than {1} characters";
		}

		protected override string GetClientRule(string element)
		{
			return "maxLength:" + MaxLength;
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			string label = ValidationSet.GetLocalizedText(element);
			return string.Format("maxLength:'{0}'", string.Format(ErrorMessageFormat, (label != null ? label : element), MaxLength)).Replace("'", "\'");
		}
	}
}
