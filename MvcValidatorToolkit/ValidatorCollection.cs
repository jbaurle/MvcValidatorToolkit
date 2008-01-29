using System;
using System.Collections.ObjectModel;

namespace System.Web.Mvc
{
	/// <summary>
	/// Represents a collection class with items of type Validator.
	/// </summary>
	public class ValidatorCollection : Collection<Validator>
	{
		/// <summary>
		/// Initializes a new instance of the ValidatorCollection class.
		/// </summary>
		public ValidatorCollection()
		{
		}

		/// <summary>
		/// Initializes a new instance of the ValidatorCollection class with optional list
		/// of Validator objects.
		/// </summary>
		public ValidatorCollection(params Validator[] validators)
		{
			foreach(Validator validator in validators)
				Add(validator);
		}

		/// <summary>
		/// Inserts a Validator into the collection at the specified index.
		/// </summary>
		protected override void InsertItem(int index, Validator validator)
		{
			if(validator == null)
				throw new ArgumentNullException("validator");

			base.InsertItem(index, validator);
		}

		/// <summary>
		/// Replaces the Validator at the specified index.
		/// </summary>
		protected override void SetItem(int index, Validator validator)
		{
			if(validator == null)
				throw new ArgumentNullException("validator");

			base.SetItem(index, validator);
		}
	}
}
