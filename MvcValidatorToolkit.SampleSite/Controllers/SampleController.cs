using System;
using System.Web;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite.Controllers
{
	public class SampleController : Controller
	{
		[ControllerAction]
		public void Sample1(string lc)
		{
			ViewData["LanguageCode"] = string.IsNullOrEmpty(lc) ? "en" : "de";

			RenderView("Sample1");
		}

		[ControllerAction]
		[ValidationSet(typeof(Sample1ValidationSet))]
		public void Sample1Processing()
		{
			if(this.ValidateForm())
				RenderView("SampleResult");
			else
				RenderView("Sample1");
		}

		[ControllerAction]
		public void Sample2()
		{
			RenderView("Sample2");
		}

		[ControllerAction]
		[ValidationSet(typeof(Sample2ValidationSet))]
		public void Sample2Processing()
		{
			if(this.ValidateForm())
				RenderView("SampleResult");
			else
				RenderView("Sample2");
		}

		[ControllerAction]
		public void Sample4()
		{
			RenderView("Sample4");
		}

		[ControllerAction]
		[ValidationSet(typeof(Sample4aValidationSet))]
		public void Sample4aProcessing()
		{
			if(this.ValidateForm())
				RenderView("SampleResult");
			else
				RenderView("Sample4");
		}

		[ControllerAction]
		[ValidationSet(typeof(Sample4bValidationSet))]
		public void Sample4bProcessing()
		{
			if(this.ValidateForm())
				RenderView("SampleResult");
			else
				RenderView("Sample4");
		}
	}
}
