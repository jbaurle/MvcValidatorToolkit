using System;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite
{
	public class Sample2ValidationSet : ValidationSet
	{
		protected override ValidatorCollection GetValidators()
		{
			return new ValidatorCollection
			(
				new ValidateElement("username, password") { Required = true, MinLength = 5, MaxLength = 30 },
				new ValidateEqualTo("password2") { ReferenceElement = "password", ErrorMessageFormat = "*" },
				new ValidatePresence("gender, membership, creditCardNumber"),
				new ValidateCreditCardNumber("creditCardNumber") { },
				new ValidatePresence("termsOfService") { ErrorMessageFormat = "*" }
			);
		}
	}
}