using System;
using System.Collections.Specialized;
using System.Text;

namespace System.Web.Mvc
{
	/// <summary>
	/// Provides extension methods for the ViewPage class used in conjunction with Validator Toolkit.
	/// </summary>
	public static class ViewPageExtensions
	{
		/// <summary>
		/// Renders the client script function to setup the client-side validation with surrounding
		/// script tags.
		/// </summary>
		public static void RenderValidationSetScripts(this ViewPage viewPage)
		{
			RenderValidationSetScripts(viewPage, true);
		}

		/// <summary>
		/// Renders the client script function to setup the client-side validation.
		/// </summary>
		public static void RenderValidationSetScripts(this ViewPage viewPage, bool addScriptTags)
		{
			// Get all defined validation sets for the derived view page class
			ValidationSetAttribute[] attributes = (ValidationSetAttribute[])viewPage.GetType().GetCustomAttributes(typeof(ValidationSetAttribute), true);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine();

			if(addScriptTags)
				sb.AppendLine("<script type=\"text/javascript\">");

			// Generate script files for each validation set
			foreach(ValidationSetAttribute vsa in attributes)
			{
				NameValueCollection errorMessages = viewPage.ViewData[vsa.ValidationSet.GetType().Name + ".ErrorMessages"] as NameValueCollection;
				sb.Append(vsa.ValidationSet.CreateClientScript(errorMessages));
			}

			if(addScriptTags)
				sb.AppendLine("</script>");

			viewPage.Response.Write(sb.ToString());
		}
	}
}
