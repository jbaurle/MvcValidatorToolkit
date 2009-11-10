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

			// Stop processing if not exactly one validation set is defined
			if(attributes.Length == 0)
				throw new Exception("The ValidateForm method expects a ValidationSetAttribute defined for the calling controller action");
			if(attributes.Length > 1)
				throw new Exception("The ValidateForm method expects just one ValidationSetAttribute defined for the calling controller action");

			// Get instance of the validation set class
			ValidationSet vs = attributes[0].ValidationSet;

			return ValidateForm(controller, vs);
		}

		public static bool ValidateForm<TValidationSet>(this Controller controller) where TValidationSet : ValidationSet
		{
			var validationSet = (TValidationSet)Activator.CreateInstance(typeof(TValidationSet));

			return ValidateForm(controller, validationSet);
		}

		public static bool ValidateForm<TValidationSet, TModel>(this Controller controller, TModel model) where TValidationSet : ValidationSet<TModel>
		{
			var validationSet = (TValidationSet)Activator.CreateInstance(typeof(TValidationSet));
			validationSet.Model = model;

			return ValidateForm(controller, validationSet);
		}

		static bool ValidateForm(Controller controller, ValidationSet vs)
		{
			// Validate HTML form (Request.Form) and save the result in the ViewData collection
			if(vs.Validate(controller.Request.Form))
				controller.ViewData[vs.GetType().Name + ".ErrorMessages"] = null;
			else
				controller.ViewData[vs.GetType().Name + ".ErrorMessages"] = vs.ErrorMessages;

			return vs.IsValid;
		}
	}
}