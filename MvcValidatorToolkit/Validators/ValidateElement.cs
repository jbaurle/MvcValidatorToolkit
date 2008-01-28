using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	public class ValidateElement : Validator
	{
		public bool Required { get; set; }
		public int MinLength { get; set; }
		public int MaxLength { get; set; }

		public ValidateElement(string element)
			: base(element)
		{
			if(string.IsNullOrEmpty(element))
				throw new ArgumentNullException("element");
		}

		public override Validator[] Translate()
		{
			string element = ElementsToValidate[0];
			List<Validator> attributes = new List<Validator>();

			if(Required)
				attributes.Add(new ValidatePresence(element));

			if(MinLength > 0 && MaxLength > 0)
				attributes.Add(new ValidateRangeLength(element, MinLength, MaxLength));
			else if(MinLength > 0)
				attributes.Add(new ValidateMinLength(element, MinLength));
			else if(MaxLength > 0)
				attributes.Add(new ValidateMaxLength(element, MaxLength));

			return attributes.ToArray();
		}

		protected override void Validate(string element)
		{
			// NOTE: This method is never called because the Translate method
			// is not returning this instance as validator to use. 
		}
	}
}
