using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validator which validates the defined element list against the given validation
	/// rules by creating the required validators.
	/// </summary>
	public class ValidateElement : Validator
	{
		public bool Required { get; set; }
		public int Min { get; set; }
		public int Max { get; set; }
		public int MinLength { get; set; }
		public int MaxLength { get; set; }

		/// <summary>
		/// Initializes a new instance of the ValidateElement class with the given elements to validate.
		/// </summary>
		public ValidateElement(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		/// <summary>
		/// Translates and returns the validator instances to use for the validation process.
		/// </summary>
		public override Validator[] Translate()
		{
			List<Validator> attributes = new List<Validator>();

			foreach(string element in ElementsToValidate)
			{
				if(Required)
					attributes.Add(new ValidatePresence(element));

				if(Min > 0 && Max > 0)
					attributes.Add(new ValidateRange(element, Min, Max));
				else if(Min > 0)
					attributes.Add(new ValidateMin(element, Min));
				else if(Max > 0)
					attributes.Add(new ValidateMax(element, Max));

				if(MinLength > 0 && MaxLength > 0)
					attributes.Add(new ValidateRangeLength(element, MinLength, MaxLength));
				else if(MinLength > 0)
					attributes.Add(new ValidateMinLength(element, MinLength));
				else if(MaxLength > 0)
					attributes.Add(new ValidateMaxLength(element, MaxLength));
			}

			return attributes.ToArray();
		}

		/// <summary>
		/// Validates the given element using the Values collection nand generates an error if 
		/// invalid.
		/// </summary>
		protected override void Validate(string element)
		{
			// NOTE: This method is never called because the Translate method is not returning 
			// this instance as validator to use.
		}
	}
}