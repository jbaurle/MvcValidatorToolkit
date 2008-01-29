using System;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validator which validates the defined element list against the given reference
	/// element.
	/// </summary>
	public class ValidateEqualTo : Validator
	{
		public string ReferenceElement { get; set; }

		/// <summary>
		/// Initializes a new instance of the ValidateDate class with the given elements to validate.
		/// </summary>
		public ValidateEqualTo(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		/// <summary>
		/// Initializes a new instance of the ValidateDate class with the given elements to validate
		/// and the reference element.
		/// </summary>
		public ValidateEqualTo(string elementsToValidate, string referenceElement)
			: base(elementsToValidate)
		{
			if(string.IsNullOrEmpty(referenceElement))
				throw new ArgumentOutOfRangeException("referenceElement");

			ReferenceElement = referenceElement;
		}

		/// <summary>
		/// Gets the rule for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientRule(string element)
		{
			return "equalTo:'#" + ReferenceElement + "'";
		}

		/// <summary>
		/// Gets the message for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientMessage(string element)
		{
			return string.Format("equalTo:'{0}'", GetLocalizedErrorMessage(element, ValidationSet.GetLocalizedText(ReferenceElement))).Replace("'", "\'");
		}

		/// <summary>
		/// Validates the given element using the Values collection and generates an error if 
		/// invalid.
		/// </summary>
		protected override void Validate(string element)
		{
			if(Values.ContainsKey(element) == false || !Values.ContainsKey(ReferenceElement) || (Values[element] ?? string.Empty) != (Values[ReferenceElement] ?? string.Empty))
				InsertError(element, ValidationSet.GetLocalizedText(ReferenceElement));
		}

		/// <summary>
		/// Gets the default error message format in English and is called if no error message is 
		/// defined in code or in App_GlobalResources.
		/// </summary>
		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain the same value as the field {1}";
		}
	}
}
