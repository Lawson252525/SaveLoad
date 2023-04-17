using System;
using System.Collections.Generic;
using System.Collections;
using JsonParse.Serialization;

namespace JsonParse.Serialization {
#region Save object main data class
	public abstract class SaveObject : ISavebleAsync, ISavebleCallback {

		private string objectKey = string.Empty;

		public string key {
			get { return this.objectKey;}
			set { this.objectKey = value;}
		}

		public ObjectContainer Save() {
			return Serialize(this);
		}

		public void Load(ObjectContainer container) {
			Deserialize(this, container);
		}

		public AsyncDeserializer LoadAsync(ObjectContainer container) {
			return DeserializeAsync(this, container);
		}

		public AsynсSerializer SaveAsync() {
			return SerializeAsync(this);
		}

		public virtual void OnBeforeSerialize() {
			
		}

		public virtual void OnAfterDeserialize() {
			
		}

		public static ObjectContainer Serialize(ISaveble saveObject) {
			using(AsynсSerializer serializer = SerializeAsync(saveObject)) {
				while(serializer.isDone == false) {}
				ObjectContainer container = serializer.GetContainer();
				return container;
			}
		}

		public static void Deserialize(ISaveble SaveObject, ObjectContainer container) {
			using(AsyncDeserializer deserializer = DeserializeAsync(SaveObject, container)) {
				while(deserializer.isDone == false) {}
			}
		}

		public static AsynсSerializer SerializeAsync(ISaveble saveObject) {
			return new AsynсSerializer(saveObject);
		}

		public static AsyncDeserializer DeserializeAsync(ISaveble saveObject, ObjectContainer container) {
			return new AsyncDeserializer(saveObject, container);
		}

	}
#endregion
}