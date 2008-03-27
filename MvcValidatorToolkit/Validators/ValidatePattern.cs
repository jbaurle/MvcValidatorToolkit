using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace System.Web.Mvc
{
    /// <summary>
    /// Regular expression validator
    /// Author: djsneed31
    /// Modifications: Bartek Szafko
    /// </summary>
    public class ValidatePattern : Validator
    {
        /// <summary>
        /// Regular expression that is tested on client
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Regular expression that is tested on client
        /// </summary>
        public string ScriptPattern { get; set; }

        /// <summary>
        /// Initializes a new instance of the ValidateDate class with the given elements to validate.
        /// </summary>
        public ValidatePattern(string elementsToValidate)
            : base(elementsToValidate)
        {}

        /// <summary>
        /// Gets a ValidatorMethodData instance that defines the client-side validator used by the
        /// jQuery validation plugin.
        /// </summary>
        public override ValidatorMethodData GetClientMethodData()
        {
            string scriptPattern = this.ScriptPattern;
            // if only server side pattern is defined use it also on clinet
            if (string.IsNullOrEmpty(scriptPattern))
            {
                scriptPattern = this.Pattern;
            }
            return new ValidatorMethodData(
                "pattern",
                "function(value,element,parameters){var re = new RegExp(/" + scriptPattern + "/); return value.match(re);}",
                "$.format('" + ErrorMessageFormat + "')"
            );
        }

        /// <summary>
        /// Gets the rule for the given element used by the jQuery validation plugin.
        /// </summary>
        public override string GetClientRule(string element)
        {
            return "pattern:true";
        }

        /// <summary>
        /// Gets the message for the given element used by the jQuery validation plugin.
        /// </summary>
        public override string GetClientMessage(string element)
        {
            return string.Format("pattern:'{0}'", GetLocalizedErrorMessage(element)).Replace("'", "\'");
        }

        /// <summary>
        /// Validates the given element using the Values collection and generates an error if 
        /// invalid.
        /// </summary>
        protected override void Validate(string element)
        {
            if (Values.ContainsKey(element) == false || (!ValidateRegex((Values[element] ?? string.Empty).Trim())))
            {
                InsertError(element);
            }
        }

        /// <summary>
        /// Checks if a value matches defined regular expression
        /// </summary>
        private bool ValidateRegex(string value)
        {
            Regex pattern = new Regex(this.Pattern);
            bool match = pattern.IsMatch(value);

            return match;
        }

        /// <summary>
        /// Gets the default error message format in English and is called if no error message is 
        /// defined in code or in App_GlobalResources.
        /// </summary>
        protected override string GetDefaultErrorMessageFormat()
        {
            return "The field {0} must match specified Pattern";
        }

    }
}
