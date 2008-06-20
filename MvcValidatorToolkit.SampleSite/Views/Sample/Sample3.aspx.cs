using System;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite.Sample
{
	[ValidationSet(typeof(Sample3aValidationSet))]
	[ValidationSet(typeof(Sample3bValidationSet))]
	public partial class Sample3 : SampleSiteViewPage
	{
	}
}
