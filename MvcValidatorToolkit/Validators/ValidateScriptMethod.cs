using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
//using System.Resources;
//using System.Text;
//using System.Web.UI;

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

		protected override ValidatorMethodData GetClientMethodData()
		{
			if(string.IsNullOrEmpty(MethodName))
				throw new Exception("MethodName must be defined for script method validator");

			return new ValidatorMethodData(
				"'scriptMethod" + TypeCount + "'",
				"function(value,element,parameters){return " + MethodName + "(value,element,parameters);}",
				"$.format('" + ErrorMessageFormat + "')"
			);
		}

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				MethodInfo mi = ValidationSet.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
										.Where(m => m.Name.ToLower() == MethodName.ToLower()).SingleOrDefault();

				if(mi != null && mi.ReturnType == typeof(bool) && mi.GetParameters().Count() == 0)
				{
					if(!(bool)mi.Invoke(ValidationSet, null))
					{
						InvalidElements.Add(element);

						string label = ValidationSet.GetLocalizedText(element);
						ErrorMessages.Add(element, string.Format(ErrorMessageFormat, (label != null ? label : element)));
					}
				}
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The script method validator for field {0} is invalid";
		}

		protected override string GetClientRule(string element)
		{
			string parameters = string.IsNullOrEmpty(Parameters) ? "true" : Parameters;

			return string.Format("scriptMethod{0}:{1}", TypeCount, parameters);
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			string label = ValidationSet.GetLocalizedText(element);
			return string.Format("scriptMethod{0}:'{1}'", TypeCount, string.Format(ErrorMessageFormat, (label != null ? label : element))).Replace("'", "\'");
		}
	}
}
