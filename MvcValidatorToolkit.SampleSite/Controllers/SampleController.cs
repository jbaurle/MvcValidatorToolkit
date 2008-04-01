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
            Sample3ViewData viewData = new Sample3ViewData();
            viewData.MembershipList = new List<MembershipData>();
            viewData.MembershipList.Add(new MembershipData("B", "Basic"));
            viewData.MembershipList.Add(new MembershipData("P", "Premium"));
            RenderView("Sample3", viewData);
        }

        [ValidationSet(typeof(Sample3ValidationSet))]
        public void Sample3Processing()
        {
            if (this.ValidateForm())
                RenderView("SampleResult");
            else
                RenderView("Sample3");
        }


		public void Sample4()
		{
			RenderView("Sample4");
		}

		[ValidationSet(typeof(Sample4aValidationSet))]
		public void Sample4aProcessing()
		{
			if(this.ValidateForm())
				RenderView("SampleResult");
			else
				RenderView("Sample4");
		}

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
