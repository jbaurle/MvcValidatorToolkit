using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;

namespace MvcValidatorToolkit.SampleSite.Controllers
{
	public class SampleController : Controller
	{
		public void Sample1(string lc)
		{
			ViewData["LanguageCode"] = string.IsNullOrEmpty(lc) ? "en" : "de";

			RenderView("Sample1");
		}

		[ValidationSet(typeof(Sample1ValidationSet))]
		public void Sample1Processing()
		{
			if(this.ValidateForm())
				RenderView("SampleResult");
			else
				RenderView("Sample1");
		}

		public void Sample2()
		{
			RenderView("Sample2");
		}

		[ValidationSet(typeof(Sample2ValidationSet))]
		public void Sample2Processing()
		{
			if(this.ValidateForm())
				RenderView("SampleResult");
			else
				RenderView("Sample2");
		}

		public void Sample3()
		{
			RenderView("Sample3");
		}

		[ValidationSet(typeof(Sample3aValidationSet))]
		public void Sample3aProcessing()
		{
			if(this.ValidateForm())
				RenderView("SampleResult");
			else
				RenderView("Sample3");
		}

		[ValidationSet(typeof(Sample3bValidationSet))]
		public void Sample3bProcessing()
		{
			if(this.ValidateForm())
				RenderView("SampleResult");
			else
				RenderView("Sample3");
		}

		void RenderView(string viewName)
		{
			View(viewName).ExecuteResult(ControllerContext);
		}

		void RenderView(string viewName, object model)
		{
			View(viewName, model).ExecuteResult(ControllerContext);
		}
	}
}
