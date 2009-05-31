using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validator which validates the defined element list against a given regular expression 
	/// pattern.
	/// </summary>
	public class ValidatePattern : Validator
	{
		public string Pattern { get; set; }
		public string ScriptPattern { get; set; }

		/// <summary>
		/// Initializes a new instance of the ValidatePattern class with the given elements to validate.
		/// </summary>
		public ValidatePattern(string elementsToValidate)
			: base(elementsToValidate)
		{ }

		/// <summary>
		/// Gets a ValidatorMethodData instance that defines the client-side validator used by the
		/// jQuery validation plugin.
		/// </summary>
		public override ValidatorMethodData GetClientMethodData()
		{
			string scriptPattern = ScriptPattern;

			if(string.IsNullOrEmpty(scriptPattern))
				scriptPattern = this.Pattern;

			return new ValidatorMethodData(
				"pattern" + TypeCount,
				"function(value,element,parameters){var re = new RegExp(/" + scriptPattern + "/); return value.match(re);}",
				"$.format('" + ErrorMessageFormat + "')"
			);
		}

		/// <summary>
		/// Gets the rule for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientRule(string element)
		{
			return string.Format("pattern{0}:true", TypeCount);
		}

		/// <summary>
		/// Gets the message for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientMessage(string element)
		{
			return string.Format("pattern{0}:'{1}'", TypeCount, GetLocalizedErrorMessage(element)).Replace("'", "\'");
		}

		/// <summary>
		/// Validates the given element using the Values collection and generates an error if 
		/// invalid.
		/// </summary>
		protected override void Validate(string element)
		{
			if(!Values.ContainsKey(element) || (!ValidateRegex((Values[element] ?? string.Empty).Trim())))
				InsertError(element);
		}

		/// <summary>
		/// Validates the given value against the regular expression.
		/// </summary>
		private bool ValidateRegex(string value)
		{
			if(string.IsNullOrEmpty(Pattern))
				return false;

			return new Regex(Pattern).IsMatch(value);
		}

		/// <summary>
		/// Gets the default error message format in English and is called if no error message is 
		/// defined in code or in App_GlobalResources.
		/// </summary>
		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must match specified Pattern";
		}

	}
}
