using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Web.Mvc
{
	public class ValidateDate : Validator
	{
		public string DateFormats { get; set; }

		public ValidateDate(string elementsToValidate)
			: base(elementsToValidate)
		{
			DateFormats = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern;
		}

		public ValidateDate(string elementsToValidate, string dateFormats)
			: base(elementsToValidate)
		{
			if(string.IsNullOrEmpty(dateFormats))
				throw new ArgumentNullException("dateFormats");

			DateFormats = dateFormats;
		}

		public override ValidatorMethodData GetClientMethodData()
		{
			string dateFormatArray = "['" + string.Join("','", GetDateFormatArray()) + "']";

			return new ValidatorMethodData(
				"dateFormat",
				"function(value,element,parameters){return Date.parseExact(value," + dateFormatArray + ");}",
				"$.format('" + ErrorMessageFormat + "')"
			);
		}

		public override string GetClientRule(string element)
		{
			return "dateFormat:true";
		}

		public override string GetClientMessage(string element)
		{
			return string.Format("dateFormat:'{0}'", GetLocalizedErrorMessage(element)).Replace("'", "\'");
		}

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

		protected override string GetDefaultErrorMessageFormat()
		{
			return "The field {0} must contain a valid date";
		}

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
