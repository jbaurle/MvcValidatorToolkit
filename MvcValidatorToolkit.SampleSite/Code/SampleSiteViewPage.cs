using System;
using System.Globalization;
using System.Threading;

namespace System.Web.Mvc
{
	public class SampleSiteViewPage : ViewPage
	{
		protected override void InitializeCulture()
		{
			base.InitializeCulture();

			CultureInfo ci;
			string lc = (ViewData["LanguageCode"] ?? string.Empty).ToString();

			if(!string.IsNullOrEmpty(lc) && lc == "de")
				ci = new CultureInfo("de-DE");
			else
				ci = new CultureInfo("en-US");

			Thread.CurrentThread.CurrentCulture =
			Thread.CurrentThread.CurrentUICulture = ci;
		}
	}
}
