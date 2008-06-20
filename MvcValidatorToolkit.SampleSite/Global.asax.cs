using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcValidatorToolkit.SampleSite
{
	public class Global : HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				 "Default",
				 "{controller}/{action}/{id}",
				 new { controller = "Home", action = "Index", id = "" }
			);
		}

		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);
		}
	}
}