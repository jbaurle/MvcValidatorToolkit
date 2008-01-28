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

		/// <summary>
		/// 
		/// </summary>
		public virtual Validator[] Translate()
		{
			return new Validator[] { this };
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual ValidatorMethodData GetClientMethodData()
		{
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual string GetClientRule(string element)
		{
			return string.Empty;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual string GetClientMessage(string element)
		{
			return string.Empty;
		}

		/// <summary>
		/// 
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
		/// 
		/// </summary>
		protected abstract void Validate(string element);

		/// <summary>
		/// 
		/// </summary>
		protected virtual void InsertError(string element, params object[] args)
		{
			ErrorMessages.Add(element, GetLocalizedErrorMessage(element, args));

			InvalidElements.Add(element);
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual string GetDefaultErrorMessageFormat()
		{
			return ValidationSet.GetType().Name + "." + GetType().Name;
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual string GetLocalizedLabel(string element)
		{
			string label = ValidationSet.GetLocalizedText(element);

			return string.IsNullOrEmpty(label) ? element : label;
		}

		/// <summary>
		/// 
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
