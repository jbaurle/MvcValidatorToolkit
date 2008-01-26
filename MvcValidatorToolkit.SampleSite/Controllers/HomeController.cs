using System;
using System.Web;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite.Controllers
{
	public class HomeController : Controller
	{
		[ControllerAction]
		public void Index()
		{
			RenderView("Index");
		}

		[ControllerAction]
		public void About()
		{
			RenderView("About");
		}
	}
}
