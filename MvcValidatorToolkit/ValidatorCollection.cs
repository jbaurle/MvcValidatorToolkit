using System;
using System.Collections.ObjectModel;

namespace System.Web.Mvc
{
	public class ValidatorCollection : Collection<Validator>
	{
		public ValidatorCollection()
		{
		}

		public ValidatorCollection(params Validator[] validators)
		{
			foreach(Validator validator in validators)
				Add(validator);
		}

		protected override void InsertItem(int index, Validator validator)
		{
			if(validator == null)
				throw new ArgumentNullException("validator");

			base.InsertItem(index, validator);
		}

		protected override void SetItem(int index, Validator validator)
		{
			if(validator == null)
				throw new ArgumentNullException("validator");

			base.SetItem(index, validator);
		}
	}
}
