using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents the base functionality of a validator class.
	/// </summary>
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

		/// <summary>
		/// Initializes a new instance of the Validator class with the given elements to validate.
		/// </summary>
		public Validator(string elementsToValidate)
		{
			ElementsToValidate = new List<string>();

			// Create a trimmed string list of eleements
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

		/// <summary>
		/// Initializes the current validator object with the specified validation set and
		/// type count.
		/// </summary>
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

		/// <summary>
		/// Translates and returns the validator instances to use for the validation process.
		/// </summary>
		public virtual Validator[] Translate()
		{
			return new Validator[] { this };
		}

		/// <summary>
		/// Gets a ValidatorMethodData instance that defines the client-side validator used by the
		/// jQuery validation plugin.
		/// </summary>
		public virtual ValidatorMethodData GetClientMethodData()
		{
			return null;
		}

		/// <summary>
		/// Gets the rule for the given element used by the jQuery validation plugin.
		/// </summary>
		public virtual string GetClientRule(string element)
		{
			return string.Empty;
		}

		/// <summary>
		/// Gets the message for the given element used by the jQuery validation plugin.
		/// </summary>
		public virtual string GetClientMessage(string element)
		{
			return string.Empty;
		}

		/// <summary>
		/// Validates the given element using the Values collection and generates an error if 
		/// invalid.
		/// </summary>
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

			// Call the abstract Validate method for elements that are not on the 
			// skip list
			foreach(string element in ElementsToValidate)
			{
				if(!skipElements.Contains(element))
					Validate(element);
			}

			if(ErrorMessages.Count > 0)
				IsValid = false;

			return IsValid;
		}

		/// <summary>
		/// Must be overridden in the derived validator in order to validate.
		/// </summary>
		protected abstract void Validate(string element);

		/// <summary>
		/// Inserts an error message for the specified element with additional arguments
		/// used by the message format.
		/// </summary>
		protected virtual void InsertError(string element, params object[] args)
		{
			ErrorMessages.Add(element, GetLocalizedErrorMessage(element, args));

			InvalidElements.Add(element);
		}

		/// <summary>
		/// Gets the default error message format in English and is called if no error message is 
		/// defined in code or in App_GlobalResources.
		/// </summary>
		protected virtual string GetDefaultErrorMessageFormat()
		{
			return ValidationSet.GetType().Name + "." + GetType().Name;
		}

		/// <summary>
		/// Gets the localized label text for the given element.
		/// </summary>
		protected virtual string GetLocalizedLabel(string element)
		{
			string label = ValidationSet.GetLocalizedText(element);

			return string.IsNullOrEmpty(label) ? element : label;
		}

		/// <summary>
		/// Gets the localized error message for the given element.
		/// </summary>
		protected virtual string GetLocalizedErrorMessage(string element, params object[] args)
		{
			List<object> al = new List<object>();
			al.Add(GetLocalizedLabel(element));
			al.AddRange(args);

			return string.Format(ErrorMessageFormat, al.ToArray());
		}
	}
}
