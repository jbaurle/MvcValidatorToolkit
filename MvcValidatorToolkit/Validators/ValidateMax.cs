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

		public override string GetClientRule(string element)
		{
			return "max:" + Max;
		}

		public override string GetClientMessage(string element)
		{
			return string.Format("max:'{0}'", GetLocalizedErrorMessage(element, Max)).Replace("'", "\'");
		}

		protected override void Validate(string element)
		{
			int value;

			if(!Values.ContainsKey(element) || string.IsNullOrEmpty(Values[element]) || !int.TryParse(Values[element], out value) || value > Max)
				InsertError(element, Max);
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value less than or equal to {1}";
		}
	}
}
