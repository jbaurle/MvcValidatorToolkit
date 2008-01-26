using System;
using System.Collections.Specialized;

namespace System.Web.Mvc
{
	public static class NameValueCollectionExtensions
	{
		public static bool ContainsKey(this NameValueCollection collection, string keyToFind)
		{
			if(collection == null)
				throw new ArgumentNullException("collection");
			if(keyToFind == null)
				throw new ArgumentNullException("keyToFind");

			foreach(string key in collection)
			{
				if(key.ToLower().Trim().Equals(keyToFind.ToLower().Trim()))
					return true;
			}

			return false;
		}
	}
}
