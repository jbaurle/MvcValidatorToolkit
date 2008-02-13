using System;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite.Sample
{
	[ValidationSet(typeof(Sample4aValidationSet))]
	[ValidationSet(typeof(Sample4bValidationSet))]
	public partial class Sample4 : SampleSiteViewPage
	{
	}
}
