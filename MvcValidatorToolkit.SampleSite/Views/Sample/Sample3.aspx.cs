using System;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite.Sample
{
	[ValidationSet(typeof(Sample3ValidationSet))]
	public partial class Sample3 : ViewPage<Sample3ViewData>
	{
	}
}
