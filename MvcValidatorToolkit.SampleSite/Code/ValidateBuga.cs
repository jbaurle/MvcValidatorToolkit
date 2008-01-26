using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite
{
	public class ValidateBuga : Validator
	{
		public ValidateBuga(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		protected override ValidatorMethodData GetClientMethodData()
		{
			return new ValidatorMethodData(
				"buga",
				"function(value,element,parameters){return value=='buga';}",
				"$.format('" + ErrorMessageFormat + "')"
			);
		}

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				if(!Values.ContainsKey(element) || (Values[element] ?? string.Empty).Trim() != "buga")
					AddError(element);
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain the value \"buga\"";
		}

		protected override string GetClientRule(string element)
		{
			return "buga:true";
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			// TODO: Change logic element is not needed?
			string label = ValidationSet.GetLocalizedText(element);
			return string.Format("buga:'{0}'", string.Format(ErrorMessageFormat, label != null ? label : element)).Replace("'", "\'");
		}
	}
}
