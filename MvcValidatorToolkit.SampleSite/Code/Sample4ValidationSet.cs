using System;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite
{
	public class Sample4aValidationSet : ValidationSet
	{
		protected override ValidatorCollection GetValidators()
		{
			return new ValidatorCollection
			(
				new ValidateElement("field1") { Required = true }
			);
		}
	}

	public class Sample4bValidationSet : ValidationSet
	{
		protected override ValidatorCollection GetValidators()
		{
			return new ValidatorCollection
			(
				new ValidateElement("field2") { Required = true }
			);
		}
	}
}