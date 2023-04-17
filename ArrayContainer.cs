using System;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;

namespace JsonParse {
#region ArrayContainer data class
		public sealed class ArrayContainer : Container, ICollection<string>, ICollection {

			[JsonProperty]
			private List<string> values = new List<string>();

			public string this [int index] {
				get { return this.values[index];}
				set { this.values[index] = value;}
			}

			public void Add(string item) {
				this.values.Add(item);
			}

			public void Clear() {
				this.values.Clear();
			}

			public bool Contains(string item) {
				return this.values.Contains(item);
			}

			public void CopyTo(string[] array, int arrayIndex) {
				this.values.CopyTo(array, arrayIndex);
			}

			public bool Remove(string item) {
				return this.values.Remove(item);
			}

			public int Count {
				get { return this.values.Count;}
			}

			public bool IsReadOnly {
				get { return false;}
			}

			public IEnumerator<string> GetEnumerator() {
				return this.values.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator() {
				return GetEnumerator();
			}

			void ICollection.CopyTo(Array array, int index) {
				CopyTo(new string[array.Length], index);
			}

			object ICollection.SyncRoot {
				get { return null;}
			}

			bool ICollection.IsSynchronized {
				get { return true;}
			}

			}
		#endregion
		}