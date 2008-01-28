using System;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite
{
	[MessageResourceName("Sample1ValidationSet")]
	public class Sample1ValidationSet : ValidationSet
	{
		protected override ValidatorCollection GetValidators()
		{
			return new ValidatorCollection
			(
				new ValidateElement("username") { Required = true, MinLength = 5, MaxLength = 10 },
				new ValidateElement("password") { Required = true, MinLength = 3, MaxLength = 10 }
			);

			// ---
			// OR you may use this way to create the same validation rules:
			//			
			//return new ValidatorCollection
			//(
			//   new ValidatePresence("username, password"),
			//   new ValidateMinLength("username", 5),
			//   new ValidateMinLength("password", 3),
			//   new ValidateMaxLength("username, password", 10)
			//);
		}
	}
}