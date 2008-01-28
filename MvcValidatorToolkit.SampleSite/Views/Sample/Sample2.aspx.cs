using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite.Sample
{
	[ValidationSet(typeof(Sample2ValidationSet))]
	public partial class Sample2 : ViewPage
	{
	}
}
