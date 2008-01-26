using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	public class ValidatePresence : Validator
	{
		public ValidatePresence(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				if(!Values.ContainsKey(element) || (Values[element] ?? string.Empty).Trim().Length == 0)
				{
					InvalidElements.Add(element);

					string label = ValidationSet.GetLocalizedText(element);
					ErrorMessages.Add(element, string.Format(ErrorMessageFormat, (label != null ? label : element)));
				}
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} is required";
		}

		protected override string GetClientRule(string element)
		{
			return "required:true";
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			string label = ValidationSet.GetLocalizedText(element);
			return string.Format("required:'{0}'", string.Format(ErrorMessageFormat, (label != null ? label : element))).Replace("'", "\'");
		}
	}
}
