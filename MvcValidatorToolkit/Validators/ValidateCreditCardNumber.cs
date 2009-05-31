using System;
using System.Text.RegularExpressions;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validator which validates the defined element list against the presence of a credit
	/// card number.
	/// </summary>
	public class ValidateCreditCardNumber : Validator
	{
		/// <summary>
		/// Initializes a new instance of the ValidateCreditCardNumber class with the given elements to validate.
		/// </summary>
		public ValidateCreditCardNumber(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		/// <summary>
		/// Gets the rule for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientRule(string element)
		{
			return "creditcard:true";
		}

		/// <summary>
		/// Gets the message for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientMessage(string element)
		{
			return string.Format("creditcard:'{0}'", GetLocalizedErrorMessage(element)).Replace("'", "\'");
		}

		/// <summary>
		/// Validates the given element using the Values collection and generates an error if 
		/// invalid.
		/// </summary>
		protected override void Validate(string element)
		{
			if(!Values.ContainsKey(element) || string.IsNullOrEmpty(Values[element]) || (!ValidateRegexForCreditCardNumber((Values[element] ?? string.Empty).Trim())))
				InsertError(element);
		}

		/// <summary>
		/// Validates the given value against the regular expression for credit card numbers.
		/// </summary>
		private bool ValidateRegexForCreditCardNumber(string value)
		{
			// See URL for regular expression of credit card numbers:
			// http://www.codeasp.net/blogs/vinay_jss/microsoft-net/90/credit-card-validation-through-regular-expression-validator-in-aspnet
			string pattern = @"^((4\d{3})|(5[1-5]\d{2})|(6011))-?\d{4}-?\d{4}-?\d{4}|3[4,7][\d\s-]{13}$";

			return new Regex(pattern).IsMatch(value);
		}

		/// <summary>
		/// Gets the default error message format in English and is called if no error message is 
		/// defined in code or in App_GlobalResources.
		/// </summary>
		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a valid credit card number";
		}
	}
}
