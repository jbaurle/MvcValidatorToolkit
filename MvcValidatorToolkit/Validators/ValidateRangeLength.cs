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

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				int length = (Values[element] ?? string.Empty).Trim().Length;

				if(!Values.ContainsKey(element) || length < MinLength || length > MaxLength)
				{
					InvalidElements.Add(element);

					string label = ValidationSet.GetLocalizedText(element);
					ErrorMessages.Add(element, string.Format(ErrorMessageFormat, (label != null ? label : element), MinLength, MaxLength));
				}
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value between {1} and {2} characters long";
		}

		protected override string GetClientRule(string element)
		{
			return "rangeLength:[" + MinLength + "," + MaxLength + "]";
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			string label = ValidationSet.GetLocalizedText(element);
			return string.Format("rangeLength:'{0}'", string.Format(ErrorMessageFormat, (label != null ? label : element), MinLength, MaxLength)).Replace("'", "\'");
		}
	}
}
