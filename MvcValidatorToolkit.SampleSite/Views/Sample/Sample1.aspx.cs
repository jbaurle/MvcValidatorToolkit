using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite.Sample
{
	[ValidationSet(typeof(Sample1ValidationSet))]
	public partial class Sample1 : ViewPage
	{
		protected override void InitializeCulture()
		{
			base.InitializeCulture();

			CultureInfo ci;
			string lc = ViewData["LanguageCode"].ToString();

			if(!string.IsNullOrEmpty(lc) && lc == "de")
				ci = new CultureInfo("de-DE");
			else
				ci = new CultureInfo("en-US");

			Thread.CurrentThread.CurrentCulture =
			Thread.CurrentThread.CurrentUICulture = ci;
		}
	}
}
