using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validator which validates the defined element list against the presence of them.
	/// </summary>
	public class ValidatePresence : Validator
	{
		/// <summary>
		/// Initializes a new instance of the ValidateDate class with the given elements to validate.
		/// </summary>
		public ValidatePresence(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		/// <summary>
		/// Gets the rule for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientRule(string element)
		{
			return "required:true";
		}

		/// <summary>
		/// Gets the message for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientMessage(string element)
		{
			return string.Format("required:'{0}'", GetLocalizedErrorMessage(element)).Replace("'", "\'");
		}

		/// <summary>
		/// Validates the given element using the Values collection and generates an error if 
		/// invalid.
		/// </summary>
		protected override void Validate(string element)
		{
			if(Values.ContainsKey(element) == false || (Values[element] ?? string.Empty).Trim().Length == 0)
				InsertError(element);
		}

		/// <summary>
		/// Gets the default error message format in English and is called if no error message is 
		/// defined in code or in App_GlobalResources.
		/// </summary>
		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} is required";
		}
	}
}
