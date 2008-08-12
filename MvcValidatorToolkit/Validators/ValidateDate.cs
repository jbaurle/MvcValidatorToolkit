using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a validator which validates the defined element list against the given date formats 
	/// respectively the culture settings of the current thread.
	/// </summary>
	public class ValidateDate : Validator
	{
		public string DateFormats { get; set; }

		/// <summary>
		/// Initializes a new instance of the ValidateDate class with the given elements to validate.
		/// </summary>
		public ValidateDate(string elementsToValidate)
			: base(elementsToValidate)
		{
			// Use thread culture for date parsing
			DateFormats = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern;
		}

		/// <summary>
		/// Initializes a new instance of the ValidateDate class with the given elements to validate
		/// and the valid date formats.
		/// </summary>
		public ValidateDate(string elementsToValidate, string dateFormats)
			: base(elementsToValidate)
		{
			if(string.IsNullOrEmpty(dateFormats))
				throw new ArgumentNullException("dateFormats");

			DateFormats = dateFormats;
		}

		/// <summary>
		/// Gets a ValidatorMethodData instance that defines the client-side validator used by the
		/// jQuery validation plugin.
		/// </summary>
		public override ValidatorMethodData GetClientMethodData()
		{
			string dateFormatArray = "['" + string.Join("','", GetDateFormatArray()) + "']";

			return new ValidatorMethodData(
				"dateFormat",
				"function(value,element,parameters){return Date.parseExact(value," + dateFormatArray + ");}",
				"$.format('" + ErrorMessageFormat + "')"
			);
		}

		/// <summary>
		/// Gets the rule for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientRule(string element)
		{
			return "dateFormat:true";
		}

		/// <summary>
		/// Gets the message for the given element used by the jQuery validation plugin.
		/// </summary>
		public override string GetClientMessage(string element)
		{
			return string.Format("dateFormat:'{0}'", GetLocalizedErrorMessage(element)).Replace("'", "\'");
		}

		/// <summary>
		/// Validates the given element using the Values collection and generates an error if 
		/// invalid.
		/// </summary>
		protected override void Validate(string element)
		{
			bool isValid = false;

			if(Values.ContainsKey(element))
			{
				string[] dateFormats = GetDateFormatArray();
				DateTime date;

				if(DateTime.TryParseExact(Values[element], dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
					isValid = true;
			}

			if(isValid == false)
				InsertError(element);
		}

		/// <summary>
		/// Gets the default error message format in English and is called if no error message is 
		/// defined in code or in App_GlobalResources.
		/// </summary>
		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a valid date";
		}

		/// <summary>
		/// Gets a string array from the date format string.
		/// </summary>
		protected string[] GetDateFormatArray()
		{
			List<string> list = new List<string>();

			if(!string.IsNullOrEmpty(DateFormats))
			{
				string[] dateFormats = DateFormats.Split(",".ToCharArray());

				foreach(string df in dateFormats)
				{
					string dateFormat = df.Trim();

					if(list.Contains(dateFormat) == false)
						list.Add(dateFormat);
				}
			}

			return list.ToArray();
		}
	}
}