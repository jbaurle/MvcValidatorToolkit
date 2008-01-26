using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace System.Web.Mvc
{
	public abstract class Validator
	{
		public List<string> ElementsToValidate { get; set; }
		public bool IsValid { get; set; }
		public string ErrorMessageFormat { get; set; }
		public NameValueCollection ErrorMessages { get; set; }
		public List<string> InvalidElements { get; set; }

		protected NameValueCollection Values { get; set; }
		protected ValidationSet ValidationSet { get; set; }
		protected int TypeCount { get; set; }

		public Validator(string elementsToValidate)
		{
			ElementsToValidate = new List<string>();

			if(!string.IsNullOrEmpty(elementsToValidate))
			{
				string[] elements = elementsToValidate.Split(",".ToCharArray());

				foreach(string e in elements)
				{
					string element = e.Trim();

					if(ElementsToValidate.Contains(element))
						throw new ArgumentException("elementsToValidate");

					if(element.Length > 0)
						ElementsToValidate.Add(element);
				}
			}
		}

		public void Initialize(ValidationSet validationSet, int typeCount)
		{
			if(validationSet == null)
				throw new ArgumentNullException("validationSet");
			if(typeCount < 0)
				throw new ArgumentNullException("typeCount");
						
			ValidationSet = validationSet;
			TypeCount = typeCount;

			if(string.IsNullOrEmpty(ErrorMessageFormat))
				ErrorMessageFormat = ValidationSet.GetLocalizedText(GetType().Name + "_DefaultErrorMessage");
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				ErrorMessageFormat = GetDefaultErrorMessageFormat();
		}

		public void AddClientMethodData(List<string> methods)
		{
			ValidatorMethodData vmd = GetClientMethodData();

			if(string.IsNullOrEmpty(vmd.Name) || string.IsNullOrEmpty(vmd.Function) || string.IsNullOrEmpty(vmd.ErrorMessage))
				throw new ArgumentException("The ValidatorMethodData instance returned by the overridden method GetClientMethodData is invalid");

			if(vmd != null)
				methods.Add(string.Format("$.validator.addMethod('{0}',{1},{2});", vmd.Name.Trim("'".ToCharArray()), vmd.Function, vmd.ErrorMessage));
		}

		public void AddClientRuleAndMessage(string element, List<string> rules, List<string> messages, ValidationSet validationSet)
		{
			if(!ElementsToValidate.Contains(element))
				return;

			ValidationSet = validationSet;

			string rule = GetClientRule(element);
			string message = GetClientMessage(element);

			if(!string.IsNullOrEmpty(rule))
				rules.Add(rule);
			if(!string.IsNullOrEmpty(message))
				messages.Add(message);
		}

		public bool Validate(NameValueCollection values, ValidationSet validationSet, List<string> skipElements)
		{
			if(values == null)
				throw new ArgumentNullException("values");
			if(validationSet == null)
				throw new ArgumentNullException("validationSet");

			Values = values;
			ValidationSet = validationSet;

			IsValid = true;
			ErrorMessages = new NameValueCollection();
			InvalidElements = new List<string>();

			Validate(skipElements);

			if(ErrorMessages.Count > 0)
				IsValid = false;

			return IsValid;
		}

		public virtual Validator[] Translate()
		{
			return new Validator[] { this };
		}

		protected virtual void AddError(string element, params object[] args)
		{
			InvalidElements.Add(element);
			ErrorMessages.Add(element, string.Format(ErrorMessageFormat, GetLocalizedLabel(element), args));
		}

		protected virtual string GetLocalizedLabel(string element)
		{
			string label = ValidationSet.GetLocalizedText(element);
			return string.IsNullOrEmpty(label) ? element : label;
		}

		protected virtual string GetDefaultErrorMessageFormat()
		{
			return ValidationSet.GetType().Name + "." + GetType().Name;
		}

		protected virtual ValidatorMethodData GetClientMethodData()
		{
			return null;
		}

		protected virtual string GetClientRule(string element)
		{
			return string.Empty;
		}

		protected virtual string GetClientMessage(string element)
		{
			return string.Empty;
		}

		protected abstract void Validate(List<string> skipElements);
	}
}
