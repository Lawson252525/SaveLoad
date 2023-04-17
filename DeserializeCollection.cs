using System;
using System.Collections;
using JsonParse.Serialization.Generic;
using System.Linq;

namespace JsonParse.Serialization {
#region DeserializeCollection data class
	public sealed class DeserializeCollection : DeserializeNode<ICollection> {

		private ArrayContainer container = null;
		private Type elementType;

		protected override IEnumerable Numerator() {
			if (this.container == null) yield break;
			this.elementType = (this.valueType.IsArray) ? this.valueType.GetElementType() : this.valueType.GetGenericArguments().Single();
			IList list = this.value as IList;
			if(list == null) yield break;
			DeserializeNode deserializer = null;
			for(int index = 0; index < this.container.Count; index++) {
				deserializer = null;
				string containerValue = this.container[index];
				object collectionItem = list[index];
				if(AsyncBase.SaveType.IsAssignableFrom(this.elementType)) {
					ISaveble saveObject = (ISaveble)collectionItem;
					if(saveObject != null) {
						string saveContainer = this.container.FirstOrDefault(e => e.Contains(saveObject.key));
						if(string.IsNullOrEmpty(saveContainer) == false) {
							deserializer = new DeserializeSaveble(saveObject, saveContainer);
						} else deserializer = new DeserializeSaveble(saveObject, containerValue);
					}
				} else if(AsyncBase.ContainerType.IsAssignableFrom(this.elementType)) {
					IContainer containerObject = (IContainer)collectionItem;
					if(containerObject != null) {
						deserializer = new DeserializeContainer(containerObject, containerValue);
					}
				} else if(AsyncBase.ConvertType.IsAssignableFrom(this.elementType)) {
					IConvertible convertObject = (IConvertible)collectionItem;
					if(convertObject != null) {
						deserializer = new DeserializeConvert(convertObject, containerValue);
					}
				} else if(AsyncBase.CollectionType.IsAssignableFrom(this.elementType)) {
					ICollection collectionObject = (ICollection)collectionItem;
					if(collectionObject != null) {
						deserializer = new DeserializeCollection(collectionObject, containerValue);
					}
				} else	deserializer = new DeserializeNode(collectionItem, containerValue);
				if(deserializer) {
					while(deserializer.isDone == false) yield return null;
					list[index] = deserializer.value;
				}
			}
			yield break;
		}

		public DeserializeCollection(ICollection collection, string data) : base(collection, string.Empty) {
			this.container = Container.Deserialize<ArrayContainer>(data);
		}

		public DeserializeCollection(ICollection collection, ArrayContainer container) : base(collection, string.Empty) {
			this.container = container;
		}

	}
#endregion
}