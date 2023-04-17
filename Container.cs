using System;
using Newtonsoft.Json;

namespace JsonParse {
#region Container main data class
	public abstract class Container : IContainer {

		public virtual string Serialize() {
			return Serialize(this);
		}

		public static string Serialize(IContainer container) {
			string result = string.Empty;
			try {
				result = JsonConvert.SerializeObject(container);
			} catch (Exception e) {
				throw e;
			}
			return result;
		}

		public static T Deserialize<T>(string data) where T : IContainer {
			return (T)Deserialize(data, typeof(T));
		}

		public static IContainer Deserialize(string data, Type type) {
			IContainer result;
			try {
				result = (IContainer)JsonConvert.DeserializeObject(data, type);
			} catch(Exception e) {
				throw e;
			}
			return result;
		}

		public static implicit operator bool(Container container) {
			return Equals(container, null) == false;
		}

	}
	#endregion
}