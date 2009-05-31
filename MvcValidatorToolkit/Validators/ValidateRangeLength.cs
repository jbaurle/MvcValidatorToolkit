using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validator which validates the defined element list against the given min and max
	/// length value.
	/// </summary>
	public class ValidateRangeLength : Validator
	{
		public int MinLength { get; set; }
		public int MaxLength { get; set; }

		/// <summary>
		/// Initializes a new instance of the ValidateRangeLength class with the given elements to validate.
		/// </summary>
		public ValidateRangeLength(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		/// <summary>
		/// Initializes a new instance of the ValidateRangeLength class with the given elements to validate
		/// and min and max length values.
		/// </summary>
		public ValidateRangeLength(string elementsToValidate, int minLength, int maxLength)
			: base(elementsToValidate)
		{
			if(maxLength < 0)
				throw new ArgumentOutOfRangeException("maxLength");
			if(minLength < 0 || minLength > maxLength)
				throw new ArgumentOutOfRangeException("minLength");

			MinLength = minLength;
			MaxLength = maxLength;
		}

		/// <summary>
		/// Gets the rule for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientRule(string element)
		{
			return "rangeLength:[" + MinLength + "," + MaxLength + "]";
		}

		/// <summary>
		/// Gets the message for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientMessage(string element)
		{
			return string.Format("rangeLength:'{0}'", GetLocalizedErrorMessage(element, MinLength, MaxLength)).Replace("'", "\'");
		}

		/// <summary>
		/// Validates the given element using the Values collection and generates an error if 
		/// invalid.
		/// </summary>
		protected override void Validate(string element)
		{
			int length = (Values[element] ?? string.Empty).Trim().Length;

			if(Values.ContainsKey(element) == false || length < MinLength || length > MaxLength)
				InsertError(element, MinLength, MaxLength);
		}

		/// <summary>
		/// Gets the default error message format in English and is called if no error message is 
		/// defined in code or in App_GlobalResources.
		/// </summary>
		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a value between {1} and {2} characters long";
		}
	}
}
