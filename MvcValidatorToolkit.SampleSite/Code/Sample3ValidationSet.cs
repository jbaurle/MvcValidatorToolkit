using System;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite
{
	public class Sample3ValidationSet : ValidationSet
	{
		string Username = "";
		string Password = "";

		protected override ValidatorCollection GetValidators()
		{
			return new ValidatorCollection
			(
				//new ValidateDate("startDate")
				//new ValidateDate("startDate", "yyyy-mm-dd,yyyymmdd")
				//new ValidateDate("startDate") { DateFormats = "yyyy-mm-dd,yyyymmdd" }
				//new ValidateBuga("username"),
				//new ValidateScriptMethod("username") { MethodName = "validateUsername" }
				//new ValidateScriptMethod("username") { MethodName = "validateUsername", Parameters = "{ABC:2, XYZ:6}" }
			);
		}

		protected bool ValidateUsername()
		{
			return true;
		}

		protected override void OnValidate()
		{
			if(Username.StartsWith("billy") && Password.StartsWith("gat"))
				throw new ValidatorException("username", "The username/password combination is not valid");
		}
	}
}