using System;
using System.Collections.Specialized;

namespace System.Web.Mvc
{
	/// <summary>
	/// Provides extension methods for the NameValueCollection class used in conjunction with Validator Toolkit.
	/// </summary>
	public static class NameValueCollectionExtensions
	{
		/// <summary>
		/// Indicates whether the collection contains the specified key.
		/// </summary>
		public static bool ContainsKey(this NameValueCollection collection, string keyToFind)
		{
			if(collection == null)
				throw new ArgumentNullException("collection");
			if(keyToFind == null)
				throw new ArgumentNullException("keyToFind");

			string k = keyToFind.ToLower().Trim();

			foreach(string key in collection)
			{
				if(key != null && key.ToLower().Trim().Equals(k))
					return true;
			}

			return false;
		}
	}
}
