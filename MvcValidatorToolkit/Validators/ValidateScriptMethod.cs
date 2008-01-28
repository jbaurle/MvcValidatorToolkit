using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Web.Mvc
{
	public class ValidateScriptMethod : Validator
	{
		public string MethodName { get; set; }
		public string Parameters { get; set; }

		public ValidateScriptMethod(string elementsToValidate)
			: base(elementsToValidate)
		{
		}

		public ValidateScriptMethod(string elementsToValidate, string methodName)
			: base(elementsToValidate)
		{
			if(string.IsNullOrEmpty(methodName))
				throw new ArgumentNullException("methodName");

			MethodName = methodName;
		}

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

		public override string GetClientRule(string element)
		{
			string parameters = string.IsNullOrEmpty(Parameters) ? "true" : Parameters;

			return string.Format("scriptMethod{0}:{1}", TypeCount, parameters);
		}

		public override string GetClientMessage(string element)
		{
			return string.Format("scriptMethod{0}:'{1}'", TypeCount, GetLocalizedErrorMessage(element)).Replace("'", "\'");
		}

		protected override void Validate(string element)
		{
			MethodInfo mi = ValidationSet.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
									.Where(m => m.Name.ToLower() == MethodName.ToLower()).SingleOrDefault();

			if(mi != null && mi.ReturnType == typeof(bool) && mi.GetParameters().Count() == 0)
			{
				if(!(bool)mi.Invoke(ValidationSet, null))
					InsertError(element);
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The script method validator for field {0} is invalid";
		}
	}
}
