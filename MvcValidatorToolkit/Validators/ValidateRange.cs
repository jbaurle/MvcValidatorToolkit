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

		public override string GetClientRule(string element)
		{
			return "range:[" + Min + "," + Max + "]";
		}

		public override string GetClientMessage(string element)
		{
			return string.Format("range:'{0}'", GetLocalizedErrorMessage(element, Min, Max)).Replace("'", "\'");
		}

		protected override void Validate(string element)
		{
			int value;

			if(!Values.ContainsKey(element) || string.IsNullOrEmpty(Values[element]) || !int.TryParse(Values[element], out value) || value < Min || value > Max)
				InsertError(element, Min, Max);
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value between {1} and {2}";
		}
	}
}
