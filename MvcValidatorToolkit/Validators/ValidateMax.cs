using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	public class ValidateMax : Validator
	{
		public int Max { get; set; }

		public ValidateMax(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		public ValidateMax(string elementsToValidate, int max)
			: base(elementsToValidate)
		{
			Max = max;
		}

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				int value;

				if(!Values.ContainsKey(element) || string.IsNullOrEmpty(Values[element]) || !int.TryParse(Values[element], out value) || value > Max)
				{
					InvalidElements.Add(element);

					string label = ValidationSet.GetLocalizedText(element);
					ErrorMessages.Add(element, string.Format(ErrorMessageFormat, (label != null ? label : element), Max));
				}
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value less than or equal to {1}";
		}

		protected override string GetClientRule(string element)
		{
			return "max:" + Max;
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			string label = ValidationSet.GetLocalizedText(element);
			return string.Format("max:'{0}'", string.Format(ErrorMessageFormat, (label != null ? label : element), Max)).Replace("'", "\'");
		}
	}
}
