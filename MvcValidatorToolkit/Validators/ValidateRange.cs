using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validator which validates the defined element list against the given min and max
	/// integer value.
	/// </summary>
	public class ValidateRange : Validator
	{
		public int Min { get; set; }
		public int Max { get; set; }

		/// <summary>
		/// Initializes a new instance of the ValidateRange class with the given elements to validate.
		/// </summary>
		public ValidateRange(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		/// <summary>
		/// Initializes a new instance of the ValidateRange class with the given elements to validate
		/// and min and max integer values.
		/// </summary>
		public ValidateRange(string elementsToValidate, int min, int max)
			: base(elementsToValidate)
		{
			Min = min;
			Max = max;
		}

		/// <summary>
		/// Gets the rule for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientRule(string element)
		{
			return "range:[" + Min + "," + Max + "]";
		}

		/// <summary>
		/// Gets the message for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientMessage(string element)
		{
			return string.Format("range:'{0}'", GetLocalizedErrorMessage(element, Min, Max)).Replace("'", "\'");
		}

		/// <summary>
		/// Validates the given element using the Values collection and generates an error if 
		/// invalid.
		/// </summary>
		protected override void Validate(string element)
		{
			int value;

			if(!Values.ContainsKey(element) || string.IsNullOrEmpty(Values[element]) || !int.TryParse(Values[element], out value) || value < Min || value > Max)
				InsertError(element, Min, Max);
		}

		/// <summary>
		/// Gets the default error message format in English and is called if no error message is 
		/// defined in code or in App_GlobalResources.
		/// </summary>
		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value between {1} and {2}";
		}
	}
}
