using System;
using System.Diagnostics;
using System.Reflection;

namespace System.Web.Mvc
{
	public static class ControllerExtensions
	{
		public static bool ValidateForm(this Controller controller)
		{
			// Get method data for calling method
			StackTrace st = new StackTrace(false);
			StackFrame fr = st.GetFrame(1);
			MethodBase mb = fr.GetMethod();

			// Get all validation set attributes
			ValidationSetAttribute[] attributes = (ValidationSetAttribute[])mb.GetCustomAttributes(typeof(ValidationSetAttribute), false);

			// At least one attribute must be defined for the calling method
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