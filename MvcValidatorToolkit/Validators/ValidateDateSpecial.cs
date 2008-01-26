using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Web.Mvc
{
	public class ValidateDateSpecial : Validator
	{
		public CultureInfo Culture { get; set; }

		string _validator;

		public ValidateDateSpecial(string elementsToValidate)
			: this(elementsToValidate, Thread.CurrentThread.CurrentUICulture)
		{
		}

		public ValidateDateSpecial(string elementsToValidate, CultureInfo culture)
			: base(elementsToValidate)
		{
			if(culture == null)
				throw new ArgumentNullException("culture");

			Culture = culture;

			switch(culture.TwoLetterISOLanguageName)
			{
				case "en":
					_validator = "date";
					break;
				case "de":
					_validator = "dateDE";
					break;
				default:
					_validator = "dateISO";
					break;
			}
		}

		protected override void Validate(List<string> skipElements)
		{
			foreach(string element in ElementsToValidate)
			{
				if(skipElements.Contains(element))
					continue;

				bool isValid = false;

				if(Values.ContainsKey(element)) 
				{
					string[] dateFormats;

					switch(Culture.TwoLetterISOLanguageName)
					{
						case "en":
							dateFormats = new string[] { "MM/dd/yyyy", "MM/dd/yy" };
							break;
						case "de":
							dateFormats = new string[] { "dd.MM.yyyy", "dd.MM.yy" };
							break;
						default:
							dateFormats = new string[] { "yyyy-MM-dd", "yyyyMMdd", "yy-MM-dd", "yyMMdd" };
							break;
					}

					DateTime date;

					if(DateTime.TryParseExact(Values[element], dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
						isValid = true;
				}

				if(!isValid)
				{
					InvalidElements.Add(element);

					string label = ValidationSet.GetLocalizedText(element);
					ErrorMessages.Add(element, string.Format(ErrorMessageFormat, (label != null ? label : element)));
				}
			}
		}

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a valid date";
		}

		protected override string GetClientRule(string element)
		{
			return _validator + ":true";
		}

		protected override string GetClientMessage(string element)
		{
			if(string.IsNullOrEmpty(ErrorMessageFormat))
				return null;

			string label = ValidationSet.GetLocalizedText(element);
			return string.Format("{0}:'{1}'", _validator, string.Format(ErrorMessageFormat, (label != null ? label : element))).Replace("'", "\'");
		}
	}
}
