using System;
using System.Collections;
using JsonParse.Serialization.Generic;

namespace JsonParse.Serialization {
#region SerializeSaveble data class
	public sealed class SerializeSaveble : SerializeNode<ISaveble> {

		protected override IEnumerable Numerator() {
			ISavebleAsync asyncSave = this.value as ISavebleAsync;
			ObjectContainer container = null;
			if(asyncSave != null) {
				AsynсSerializer serializer = asyncSave.SaveAsync();
				while(serializer.isDone == false) yield return null;
				container = serializer.GetContainer();
			} else {
				container = this.value.Save();
			}
			if(container) this.result = container.Serialize();
			yield break;
		}

		public SerializeSaveble(ISaveble value) : base(value.key, value) {}

		public SerializeSaveble(string key, ISaveble value) : base(key, value) {}

	}
#endregion
	}