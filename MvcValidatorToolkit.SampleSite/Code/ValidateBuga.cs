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

		public override ValidatorMethodData GetClientMethodData()
		{
			return new ValidatorMethodData(
				"buga",
				"function(value,element,parameters){return value=='buga';}",
				"$.format('" + ErrorMessageFormat + "')"
			);
		}

		public override string GetClientRule(string element)
		{
			return "buga:true";
		}

		public override string GetClientMessage(string element)
		{
			return string.Format("buga:'{0}'", GetLocalizedErrorMessage(element)).Replace("'", "\'");
		}

		protected override void Validate(string element)
		{
			if(Values.ContainsKey(element) == false || (Values[element] ?? string.Empty).Trim() != "buga")
				InsertError(element);
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain the value \"buga\"";
		}
	}
}
