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

		public override string GetClientRule(string element)
		{
			return "min:" + Min;
		}

		public override string GetClientMessage(string element)
		{
			return string.Format("min:'{0}'", GetLocalizedErrorMessage(element, Min)).Replace("'", "\'");
		}

		protected override void Validate(string element)
		{
			int value;

			if(!Values.ContainsKey(element) || string.IsNullOrEmpty(Values[element]) || !int.TryParse(Values[element], out value) || value < Min)
				InsertError(element, Min);
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value greater than or equal to {1}";
		}
	}
}
