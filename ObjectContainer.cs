using System;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace JsonParse {
#region Object container data class
	public class ObjectContainer : Container {

		public string key { get; private set;}

		[JsonProperty]
		private Note values = new Note();

		public void Add(string key, string value) {
			this.values.Add(key, value);
		}

		public void Add(string key, IContainer container) {
			Add(key, container.Serialize());
		}

		public bool Contains(string key) {
			return this.values.ContainsKey(key);
		}

		[JsonIgnore]
		public int Count {
			get { return this.values.Count;}
		}

		public void Clear() {
			this.values.Clear();
		}

		public void Remove(string key) {
			this.values.Remove(key);
		}

		public string this [string key] {
			get { return this.values[key].ToString();}
			set { this.values[key] = value;}
		}

		public ObjectContainer(string key) {
			this.key = key;
		}

		public ObjectContainer() {
			this.key = GetType().ToString();
		}
			
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator() {
			foreach(DictionaryEntry pair in this.values) {
				yield return new KeyValuePair<string, string>(pair.Key.ToString(), pair.Value.ToString());
			}
		}

		public static ObjectContainer CreateInstance(string data) {
			return Container.Deserialize<ObjectContainer>(data);
		}

	}
#endregion
}