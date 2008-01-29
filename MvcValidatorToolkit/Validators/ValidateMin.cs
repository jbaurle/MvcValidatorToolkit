using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validator which validates the defined element list against the given min integer 
	/// value.
	/// </summary>
	public class ValidateMin : Validator
	{
		public int Min { get; set; }

		/// <summary>
		/// Initializes a new instance of the ValidateDate class with the given elements to validate.
		/// </summary>
		public ValidateMin(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		/// <summary>
		/// Initializes a new instance of the ValidateDate class with the given elements to validate
		/// and min integer value.
		/// </summary>
		public ValidateMin(string elementsToValidate, int min)
			: base(elementsToValidate)
		{
			Min = min;
		}

		/// <summary>
		/// Gets the rule for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientRule(string element)
		{
			return "min:" + Min;
		}

		/// <summary>
		/// Gets the message for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientMessage(string element)
		{
			return string.Format("min:'{0}'", GetLocalizedErrorMessage(element, Min)).Replace("'", "\'");
		}

		/// <summary>
		/// Validates the given element using the Values collection and generates an error if 
		/// invalid.
		/// </summary>
		protected override void Validate(string element)
		{
			int value;

			if(!Values.ContainsKey(element) || string.IsNullOrEmpty(Values[element]) || !int.TryParse(Values[element], out value) || value < Min)
				InsertError(element, Min);
		}

		/// <summary>
		/// Gets the default error message format in English and is called if no error message is 
		/// defined in code or in App_GlobalResources.
		/// </summary>
		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value greater than or equal to {1}";
		}
	}
}
