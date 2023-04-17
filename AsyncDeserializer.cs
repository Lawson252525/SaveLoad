using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;

namespace JsonParse.Serialization {
#region AsyncDeserializer data class
	public sealed class AsyncDeserializer : AsyncBase {

		protected override IEnumerable Numerator() {
			yield return null;
			foreach(KeyValuePair<string, string> pair in this.container) {
				string name = pair.Key;
				string value = pair.Value;
				Type objectType = this.value.GetType();
				while(SaveType.IsAssignableFrom(objectType)) {
					FieldInfo field = objectType.GetField(name, Flags);
					if(field != null) {
						Type fieldType = field.FieldType;
						DeserializeNode deserializer = null;
						if(SaveType.IsAssignableFrom(fieldType)) {
							ISaveble saveObject = (ISaveble)field.GetValue(this.value);
							if(saveObject != null) deserializer = new DeserializeSaveble(saveObject, value);
						}	else if (fieldType.IsEnum) {
							Enum enumerator = (Enum)field.GetValue(this.value);
							deserializer = new DeserializeEnum(enumerator, value);
						} else if(ContainerType.IsAssignableFrom(fieldType)) {
							IContainer containerObject = (IContainer)field.GetValue(this.value);
							if(containerObject != null) deserializer = new DeserializeContainer(containerObject, value);
						} else if(ConvertType.IsAssignableFrom(fieldType)) {
							IConvertible convertObject = (IConvertible)field.GetValue(this.value);
							if(convertObject != null) deserializer = new DeserializeConvert(convertObject, value);
						} else if(CollectionType.IsAssignableFrom(fieldType)) {
							ICollection collection = (ICollection)field.GetValue(this.value);
							if(collection != null) deserializer = new DeserializeCollection(collection, value);
						} else {
							deserializer = new DeserializeNode(field.GetValue(this.value), value);
						}
						if(deserializer) {
							while(deserializer.isDone == false) yield return null;
							field.SetValue(this.value, deserializer.value);
						}
						break;
					}
					objectType = objectType.BaseType;
				}
				yield return null;
			}
			yield return null;
			if(this.value is ISavebleCallback) {
				ISavebleCallback callback = this.value as ISavebleCallback;
				callback.OnAfterDeserialize();
			}
		}

		public AsyncDeserializer(ISaveble saveObject, ObjectContainer container) : base(saveObject) {
			this.container = container;
		}

	}
#endregion
}