using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace System.Web.Mvc
{
	public abstract class ValidationSet
	{
		protected static string DefaultMessageResourceName = "ValidationSet";

		public bool IsValid { get; set; }
		public NameValueCollection ErrorMessages { get; set; }
		public List<string> InvalidElements { get; set; }
		public NameValueCollection Values { get; set; }

		protected Dictionary<Type, int> UsedTypes { get; set; }
		protected ResourceManager ResourceManager { get; set; }
		protected ResourceManager ResourceManagerDefault { get; set; }

		protected virtual ValidatorCollection GetValidators()
		{
			return new ValidatorCollection();
		}

		public string CreateClientScript(NameValueCollection errorMessages)
		{
			List<string> methods = new List<string>();
			List<string> methodsCreated = new List<string>();
			OrderedDictionary rules = new OrderedDictionary();
			OrderedDictionary messages = new OrderedDictionary();
			Dictionary<string, List<Type>> handledElements = new Dictionary<string, List<Type>>();

			List<Validator> validators = new List<Validator>();
			foreach(Validator validator in GetValidators().ToArray())
				validators.AddRange(validator.Translate());

			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("function updateSettingsFor{0}(v){{", GetType().Name);
			sb.AppendLine();

			foreach(Validator validator in validators.ToArray())
			{
				InitialzeValidator(validator);

				ValidatorMethodData vmd = validator.GetClientMethodData();

				if(vmd != null)
				{
					if(string.IsNullOrEmpty(vmd.Name) || string.IsNullOrEmpty(vmd.Function) || string.IsNullOrEmpty(vmd.ErrorMessage))
						throw new ArgumentException(string.Format("The ValidatorMethodData instance returned for validator type {0} is invalid", validator.GetType()));

					if(!methodsCreated.Contains(vmd.Name))
					{
						methods.Add(string.Format("$.validator.addMethod('{0}',{1},{2});", vmd.Name.Trim("'".ToCharArray()), vmd.Function, vmd.ErrorMessage));
						methodsCreated.Add(vmd.Name);
					}
				}

				if(validator.ElementsToValidate != null)
				{
					foreach(string element in validator.ElementsToValidate)
					{
						// Avoid creating and adding client data if element is used in 
						// multiple validation cases
						if(handledElements.ContainsKey(element))
						{
							if(handledElements[element].Contains(validator.GetType()))
								throw new Exception(string.Format(
									"Element {0} is used in multiple instances of validator {1}",
									element, validator.GetType()));
						}
						else
						{
							handledElements.Add(element, new List<Type>());
							handledElements[element].Add(validator.GetType());
						}

						if(rules.Contains(element) == false)
							rules.Add(element, new List<string>());
						if(messages.Contains(element) == false)
							messages.Add(element, new List<string>());

						string rule = validator.GetClientRule(element);
						string message = validator.GetClientMessage(element);

						if(!string.IsNullOrEmpty(rule))
							((List<string>)rules[element]).Add(rule);
						if(!string.IsNullOrEmpty(message))
							((List<string>)messages[element]).Add(message);
					}
				}
			}

			foreach(string method in methods)
				sb.AppendFormat("\t{0}", method).AppendLine();

			sb.AppendLine("\tif(typeof(v)!='object')");
			sb.AppendLine("\t\treturn null;");

			foreach(DictionaryEntry de in rules)
			{
				string list = string.Join("," + Environment.NewLine + "\t\t\t", ((List<string>)de.Value).ToArray());

				if(!string.IsNullOrEmpty(list))
				{
					sb.AppendFormat("\tif(v.settings.rules.{0}==undefined){{", de.Key.ToString()).AppendLine();
					sb.AppendFormat("\t\tv.settings.rules.{0}={{", de.Key.ToString()).AppendLine();
					sb.AppendFormat("\t\t\t{0}", list).AppendLine();
					sb.AppendLine("\t\t};");
					sb.AppendLine("\t}");
				}
			}

			foreach(DictionaryEntry de in messages)
			{
				string list = string.Join("," + Environment.NewLine + "\t\t\t", ((List<string>)de.Value).ToArray());

				if(!string.IsNullOrEmpty(list))
				{
					sb.AppendFormat("\tif(v.settings.messages.{0}==undefined){{", de.Key.ToString()).AppendLine();
					sb.AppendFormat("\t\tv.settings.messages.{0}={{", de.Key.ToString()).AppendLine();
					sb.AppendFormat("\t\t\t{0}", list).AppendLine();
					sb.AppendLine("\t\t};");
					sb.AppendLine("\t}");
				}
			}

			if(errorMessages != null && errorMessages.Count > 0)
			{
				List<string> list = new List<string>();

				foreach(string element in errorMessages.AllKeys)
					list.Add(element + ":'" + errorMessages[element] + "'");

				sb.AppendFormat("\tv.showErrors({{{0}}});", string.Join(",", list.ToArray()));
				sb.AppendLine();
			}

			sb.AppendLine("\treturn v;");
			sb.AppendLine("}");

			return sb.ToString();
		}

		public bool Validate(NameValueCollection values)
		{
			IsValid = true;
			ErrorMessages = new NameValueCollection();
			InvalidElements = new List<string>();

			if(values == null)
				throw new ArgumentNullException("values");
			else
				Values = values;

			UpdateFields();

			List<Validator> validators = new List<Validator>();
			foreach(Validator validator in GetValidators().ToArray())
				validators.AddRange(validator.Translate());

			foreach(Validator validator in validators.ToArray())
			{
				InitialzeValidator(validator);

				if(!validator.Validate(Values, this, InvalidElements))
				{
					IsValid = false;

					foreach(string element in validator.ErrorMessages.AllKeys)
					{
						if(ErrorMessages.ContainsKey(element)==false)
							ErrorMessages.Add(element, validator.ErrorMessages[element]);
					}

					UpdateInvalidElements(validator.InvalidElements.ToArray());
				}
			}

			try
			{
				OnValidate();
			}
			catch(ValidatorException e)
			{
				IsValid = false;

				if(!string.IsNullOrEmpty(e.InvalidElement) && !InvalidElements.Contains(e.InvalidElement))
				{
					ErrorMessages.Add(e.InvalidElement, e.Message);
					UpdateInvalidElements(new string[] { e.InvalidElement });
				}
			}

			return IsValid;
		}

		protected virtual void OnValidate()
		{
		}

		public virtual string GetLocalizedText(string key)
		{
			if(ResourceManagerDefault == null && ResourceManager == null)
			{
				Assembly assembly = null;

				try
				{
					assembly = Assembly.Load("App_GlobalResources");
				}
				catch { return null; }

				MessageResourceNameAttribute[] attributes =
					(MessageResourceNameAttribute[])GetType().GetCustomAttributes(typeof(MessageResourceNameAttribute), true);

				if(attributes.Length > 0)
					ResourceManager = new ResourceManager("Resources." + attributes[0].ResourceName, assembly);

				ResourceManagerDefault = new ResourceManager("Resources." +
					(string.IsNullOrEmpty(DefaultMessageResourceName) ? "ValidationSet" : DefaultMessageResourceName), assembly);
			}

			string text;

			if(ResourceManager != null)
			{
				try
				{
					if((text = ResourceManager.GetString(key)) != null)
						return text;
				}
				catch { }
			}

			if(ResourceManagerDefault != null)
			{
				try
				{
					if((text = ResourceManagerDefault.GetString(GetType().Name + "_" + key)) != null)
						return text;
				}
				catch { }

				try
				{
					if((text = ResourceManagerDefault.GetString(key)) != null)
						return text;
				}
				catch { }
			}

			return null;
		}

		void UpdateFields()
		{
			FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

			foreach(FieldInfo field in fields)
			{
				if(field.FieldType == typeof(string) && Values.ContainsKey(field.Name))
					field.SetValue(this, Values[field.Name]);
			}
		}

		void UpdateInvalidElements(string[] elements)
		{
			foreach(string element in elements)
			{
				if(InvalidElements.Contains(element) == false)
					InvalidElements.Add(element);
			}
		}

		void InitialzeValidator(Validator validator)
		{
			if(UsedTypes == null)
				UsedTypes = new Dictionary<Type, int>();

			Type vt = validator.GetType();

			if(UsedTypes.ContainsKey(vt))
				UsedTypes[vt] += 1;
			else
				UsedTypes.Add(vt, 1);

			validator.Initialize(this, UsedTypes[vt]);
		}
	}
}
