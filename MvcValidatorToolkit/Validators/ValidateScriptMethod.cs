using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validator which validates the defined element list against the custom method.
	/// </summary>
	public class ValidateScriptMethod : Validator
	{
		public string MethodName { get; set; }
		public string Parameters { get; set; }

		/// <summary>
		/// Initializes a new instance of the ValidateDate class with the given elements to validate.
		/// </summary>
		public ValidateScriptMethod(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		/// <summary>
		/// Initializes a new instance of the ValidateDate class with the given elements to validate
		/// and the custom method.
		/// </summary>
		public ValidateScriptMethod(string elementsToValidate, string methodName)
			: base(elementsToValidate)
		{
			if(string.IsNullOrEmpty(methodName))
				throw new ArgumentNullException("methodName");

			MethodName = methodName;
		}

		/// <summary>
		/// Gets a ValidatorMethodData instance that defines the client-side validator used by the
		/// jQuery validation plugin.
		/// </summary>
		public override ValidatorMethodData GetClientMethodData()
		{
			if(string.IsNullOrEmpty(MethodName))
				throw new Exception("MethodName must be defined for script method validator");

			return new ValidatorMethodData(
				"scriptMethod" + TypeCount,
				"function(value,element,parameters){return " + MethodName + "(value,element,parameters);}",
				"$.format('" + ErrorMessageFormat + "')"
			);
		}

		/// <summary>
		/// Gets the rule for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientRule(string element)
		{
			string parameters = string.IsNullOrEmpty(Parameters) ? "true" : Parameters;

			return string.Format("scriptMethod{0}:{1}", TypeCount, parameters);
		}

		/// <summary>
		/// Gets the message for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientMessage(string element)
		{
			return string.Format("scriptMethod{0}:'{1}'", TypeCount, GetLocalizedErrorMessage(element)).Replace("'", "\'");
		}

		/// <summary>
		/// Validates the given element using the Values collection and generates an error if 
		/// invalid.
		/// </summary>
		protected override void Validate(string element)
		{
			// TODO: Call method with parameters or null

			MethodInfo mi = ValidationSet.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
									.Where(m => m.Name.ToLower() == MethodName.ToLower()).SingleOrDefault();

			if(mi != null && mi.ReturnType == typeof(bool) && mi.GetParameters().Count() == 0)
			{
				if(!(bool)mi.Invoke(ValidationSet, null))
					InsertError(element);
			}
		}

		/// <summary>
		/// Gets the default error message format in English and is called if no error message is 
		/// defined in code or in App_GlobalResources.
		/// </summary>
		protected override string GetDefaultErrorMessageFormat()
		{
			return "The script method validator for field {0} is invalid";
		}
	}
}
