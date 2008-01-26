using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	public class ValidateMin : Validator
	{
		public int Min { get; set; }

		public ValidateMin(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		public ValidateMin(string elementsToValidate, int min)
			: base(elementsToValidate)
		{
			Min = min;
		}

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				int value;

				if(!Values.ContainsKey(element) || string.IsNullOrEmpty(Values[element]) || !int.TryParse(Values[element], out value) || value < Min)
				{
					InvalidElements.Add(element);

					string label = ValidationSet.GetLocalizedText(element);
					ErrorMessages.Add(element, string.Format(ErrorMessageFormat, (label != null ? label : element), Min));
				}
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value greater than or equal to {1}";
		}

		protected override string GetClientRule(string element)
		{
			return "min:" + Min;
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			string label = ValidationSet.GetLocalizedText(element);
			return string.Format("min:'{0}'", string.Format(ErrorMessageFormat, (label != null ? label : element), Min)).Replace("'", "\'");
		}
	}
}
