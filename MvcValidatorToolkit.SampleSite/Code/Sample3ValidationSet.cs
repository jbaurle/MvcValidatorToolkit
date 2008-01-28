using System;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite
{
	//[MessageOrder("...")]
	//[MessageResourceName("...")]
	public class Sample3ValidationSet : ValidationSet
	{
		string Username = "";
		string Password = "";

		protected override ValidatorCollection GetValidators()
		{
			return new ValidatorCollection
			(
				// NOTE: What happens if more than one Buga tests are checked?????

				new ValidatePresence("username, password"),
				new ValidateMinLength("username", 5),
				new ValidateMinLength("password", 3)

		
				//new ValidateRange("password", 11, 15)
				//new ValidateMin("password", 11),
				//new ValidateMax("password", 15)
				//new ValidateRemote("password","~/Home/Check")
				//new ValidateRemote("password") { Url = "~/Home/Check" }
				//new ValidateRangeLength("password") { MinLength = 3, MaxLength = 5 }
				//new ValidateEqualTo("password") { ReferenceElement = "username" }
				//new ValidateDateSpecial("username", new CultureInfo("en-US"))
				//new ValidateDate("username")
				//new ValidateDate("username", "yyyy-mm-dd,yyyymmdd")
				//new ValidateDate("username") { DateFormats = "yyyy-mm-dd,yyyymmdd" }
				//new ValidateBuga("username"),
				//new ValidateScriptMethod("username") { MethodName = "validateUsername" }
				//new ValidateScriptMethod("username") { MethodName = "onValidateUsername", Parameters = "{ABC:2, XYZ:6}" }
				//new ValidateBuga("username")
				//new ValidateElement("username") { Required = true, MinLength = 5, MaxLength = 30 },
				//new ValidateElement("password") { Required = true, MinLength = 3 }
			);
		}

		protected bool ValidateUsername()
		{
			return false;
		}

		protected override void OnValidate()
		{
			// TODO: Change!
			if(Username.StartsWith("jbau") && Password.StartsWith("pac"))
				throw new ValidatorException("username", "The combination is not valid");
		}
	}
}