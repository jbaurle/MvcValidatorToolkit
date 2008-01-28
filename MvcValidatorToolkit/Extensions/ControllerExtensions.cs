using System;
using System.Diagnostics;
using System.Reflection;

namespace System.Web.Mvc
{
	/// <summary>
	/// Provides extension methods for the Controller class used in conjunction with Validator Toolkit.
	/// </summary>
	public static class ControllerExtensions
	{
		/// <summary>
		/// Validates the HTML form data (Request.Form) using the validation set defined for the 
		/// calling controller action. This mehtod must be called within a controller action.
		/// </summary>
		public static bool ValidateForm(this Controller controller)
		{
			// Get method data for calling method (should be a controller action)
			StackTrace st = new StackTrace(false);
			StackFrame fr = st.GetFrame(1);
			MethodBase mb = fr.GetMethod();

			// Get the validation set attribute
			ValidationSetAttribute[] attributes = (ValidationSetAttribute[])mb.GetCustomAttributes(typeof(ValidationSetAttribute), false);

			// Stop processing if none or more than one validation set is defined
			if(attributes.Length == 0)
				throw new Exception("The ValidateForm method expects a ValidationSetAttribute defined for the calling controller action");
			if(attributes.Length > 1)
				throw new Exception("The ValidateForm method expects just one ValidationSetAttribute defined for the calling controller action");

			ValidationSet vs = attributes[0].ValidationSet;

			if(vs.Validate(controller.Request.Form))
				controller.ViewData[vs.GetType().Name + ".ErrorMessages"] = null;
			else
				controller.ViewData[vs.GetType().Name + ".ErrorMessages"] = vs.ErrorMessages;

			return vs.IsValid;
		}
	}
}