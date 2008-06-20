using System;
using System.Collections.Generic;
using System.Web.UI;

namespace MvcValidatorToolkit.SampleSite
{
	public partial class DefaultPage : Page
	{
		public void Page_Load(object sender, EventArgs e)
		{
			Response.Redirect("~/Home");
		}
	}
}
