using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JsonParse {
#region Note data class
	[System.Serializable]
	public sealed class Note : Hashtable, ISerializable {

		public override void GetObjectData(SerializationInfo info, StreamingContext context) {
			foreach(DictionaryEntry pair in this) {
				info.AddValue(pair.Key.ToString(), pair.Value.ToString());
			}
		}

		public Note(SerializationInfo info, StreamingContext context) {
			foreach(SerializationEntry pair in info) {
				Add(pair.Name, pair.Value.ToString());
			}
		}

		public Note() : base() {}

	}
#endregion
}