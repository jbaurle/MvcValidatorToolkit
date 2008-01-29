using System;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validator which validates the defined element list against the given max integer 
	/// value.
	/// </summary>
	public class ValidateMax : Validator
	{
		public int Max { get; set; }

		/// <summary>
		/// Initializes a new instance of the ValidateDate class with the given elements to validate.
		/// </summary>
		public ValidateMax(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		/// <summary>
		/// Initializes a new instance of the ValidateDate class with the given elements to validate
		/// and the max value.
		/// </summary>
		public ValidateMax(string elementsToValidate, int max)
			: base(elementsToValidate)
		{
			Max = max;
		}

		/// <summary>
		/// Gets the rule for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientRule(string element)
		{
			return "max:" + Max;
		}

		/// <summary>
		/// Gets the message for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientMessage(string element)
		{
			return string.Format("max:'{0}'", GetLocalizedErrorMessage(element, Max)).Replace("'", "\'");
		}

		/// <summary>
		/// Validates the given element using the Values collection and generates an error if 
		/// invalid.
		/// </summary>
		protected override void Validate(string element)
		{
			int value;

			if(!Values.ContainsKey(element) || string.IsNullOrEmpty(Values[element]) || !int.TryParse(Values[element], out value) || value > Max)
				InsertError(element, Max);
		}

		/// <summary>
		/// Gets the default error message format in English and is called if no error message is 
		/// defined in code or in App_GlobalResources.
		/// </summary>
		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value less than or equal to {1}";
		}
	}
}
