using System;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite.Sample
{
	[ValidationSet(typeof(Sample1ValidationSet))]
	public partial class Sample1 : SampleSiteViewPage
	{
	}
}
