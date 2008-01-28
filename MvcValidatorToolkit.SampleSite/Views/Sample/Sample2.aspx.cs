using System;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite.Sample
{
	[ValidationSet(typeof(Sample2ValidationSet))]
	public partial class Sample2 : SampleSiteViewPage
	{
	}
}
