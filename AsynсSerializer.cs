using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;

namespace JsonParse.Serialization {
#region AsyncSerializer data class
	public sealed class AsynсSerializer : AsyncBase {

		public ObjectContainer GetContainer() {
			return this.container;
		}

		protected override IEnumerable Numerator() {
			yield return null;
			if(this.value is ISavebleCallback) {
				ISavebleCallback callback = this.value as ISavebleCallback;
				callback.OnBeforeSerialize();
			}
			this.container = new ObjectContainer(this.saveObject.key);
			Type objectType = this.value.GetType();
			while(SaveType.IsAssignableFrom(objectType)) {
				FieldInfo[] fields = objectType.GetFields(Flags).Where(f => f.IsDefined(AttributeType, true) && this.container.Contains(f.Name) == false).ToArray();
				SerializeNode serializer = null;
				foreach(FieldInfo field in fields) {
					Type fieldType = field.FieldType;
					string key = field.Name;
					serializer = null;
					if(SaveType.IsAssignableFrom(fieldType)) {
						ISaveble save = (ISaveble)field.GetValue(this.value);
						if(save != null) serializer = new SerializeSaveble(key, save);
					} else if(ContainerType.IsAssignableFrom(fieldType)) {
						IContainer containerObject = (IContainer)field.GetValue(this.value);
						if(containerObject != null) serializer = new SerializeContainer(key, containerObject);
					} else if(ConvertType.IsAssignableFrom(fieldType)) {
						IConvertible convertItem = (IConvertible)field.GetValue(this.value);
						serializer = new SerailizeConvert(key, convertItem);
					} else if(CollectionType.IsAssignableFrom(fieldType)) {
						ICollection collection = (ICollection)field.GetValue(this.value);
						serializer = new SerializeCollection(key, collection);
					} else serializer = new SerializeNode(key, field.GetValue(this.value));
					if(serializer) {
						while(serializer.isDone == false) yield return null;
						if(string.IsNullOrEmpty(serializer.result) == false) {
							this.container.Add(key, serializer.result);
						}
					}
				}
				objectType = objectType.BaseType;
			}
			yield return null;
		}

		public AsynсSerializer(ISaveble saveObject) : base(saveObject) {}

	}
#endregion
}