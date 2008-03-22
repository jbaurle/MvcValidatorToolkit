using System;
using System.Web;
using System.Web.Mvc;

namespace MvcValidatorToolkit.SampleSite.Controllers
{
	public class HomeController : Controller
	{
		public void Index()
		{
			RenderView("Index");
		}

		public void About()
		{
			RenderView("About");
		}
	}
}
