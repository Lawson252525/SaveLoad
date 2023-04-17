using System;
using System.Collections;
using System.Linq;
using JsonParse.Serialization.Generic;

namespace JsonParse.Serialization {
#region SerializeCollection data class
	public sealed class SerializeCollection : SerializeNode<ICollection> {

		private readonly Type elementType;

		private ArrayContainer arrayContainer = new ArrayContainer();

		protected override IEnumerable Numerator() {
			foreach(object collectionItem in this.value) {
				SerializeNode serializer = null;
				if(AsyncBase.SaveType.IsAssignableFrom(this.elementType)) {
					ISaveble saveObject = (ISaveble)collectionItem;
					if(saveObject != null) serializer = new SerializeSaveble(saveObject);
				} else if(AsyncBase.ContainerType.IsAssignableFrom(this.elementType)) {
					IContainer containerObject = (IContainer)collectionItem;
					if(containerObject != null) serializer = new SerializeContainer(this.key, containerObject);
				} else if(AsyncBase.ConvertType.IsAssignableFrom(this.elementType)) {
					IConvertible convertItem = (IConvertible)collectionItem;
					if(convertItem != null) serializer = new SerailizeConvert(this.key, convertItem);
				} else if(AsyncBase.CollectionType.IsAssignableFrom(this.elementType)) {
					ICollection collection = (ICollection)collectionItem;
					if(collection != null) serializer = new SerializeCollection(this.key, collection);
				} else serializer = new SerializeNode(this.key, collectionItem);
				if(serializer) {
					while(serializer.isDone == false) yield return null;
				}
				if(string.IsNullOrEmpty(serializer.result) == false) {
					this.arrayContainer.Add(serializer.result);
				}
			}
			if(this.arrayContainer.Count > 0) {
				this.result = this.arrayContainer.Serialize();
			}
			yield break;
		}

		public SerializeCollection(string key, ICollection value) : base(key, value) {
			Type collectionType = this.value.GetType();
			this.elementType = (collectionType.IsArray) ? collectionType.GetElementType() : collectionType.GetGenericArguments().Single();
		}

	}
#endregion
}