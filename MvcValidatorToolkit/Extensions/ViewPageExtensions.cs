using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;

namespace System.Web.Mvc
{
	public static class ViewPageExtensions
	{
		public static void RenderValidationSetScripts(this ViewPage viewPage)
		{
			RenderValidationSetScripts(viewPage, true);
		}

		public static void RenderValidationSetScripts(this ViewPage viewPage, bool addScriptTags)
		{
			ValidationSetAttribute[] attributes =
				(ValidationSetAttribute[])viewPage.GetType().GetCustomAttributes(typeof(ValidationSetAttribute), true);

			StringBuilder sb = new StringBuilder();

			sb.AppendLine();

			if(addScriptTags)
				sb.AppendLine("<script type=\"text/javascript\">");

			foreach(ValidationSetAttribute vsa in attributes)
			{
				NameValueCollection errorMessages = viewPage.ViewData[vsa.ValidationSet.GetType().Name + ".ErrorMessages"] as NameValueCollection;
				sb.Append(vsa.ValidationSet.CreateClientScript(errorMessages));
			}

			if(addScriptTags)
				sb.AppendLine("</script>");

			string script = sb.ToString();

			viewPage.Response.Write(script);
		}
	}
}
