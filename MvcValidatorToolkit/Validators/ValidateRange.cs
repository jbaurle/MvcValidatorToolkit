using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	public class ValidateRange : Validator
	{
		public int Min { get; set; }
		public int Max { get; set; }

		public ValidateRange(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		public ValidateRange(string elementsToValidate, int min, int max)
			: base(elementsToValidate)
		{
			Min = min;
			Max = max;
		}

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				int value;

				if(!Values.ContainsKey(element) || string.IsNullOrEmpty(Values[element]) || !int.TryParse(Values[element], out value) || value < Min || value > Max)
				{
					InvalidElements.Add(element);

					string label = ValidationSet.GetLocalizedText(element);
					ErrorMessages.Add(element, string.Format(ErrorMessageFormat, (label != null ? label : element), Min, Max));
				}
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value between {1} and {2}";
		}

		protected override string GetClientRule(string element)
		{
			return "range:[" + Min + "," + Max + "]";
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			string label = ValidationSet.GetLocalizedText(element);
			return string.Format("range:'{0}'", string.Format(ErrorMessageFormat, (label != null ? label : element), Min, Max)).Replace("'", "\'");
		}
	}
}
