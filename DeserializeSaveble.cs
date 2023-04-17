using System;
using JsonParse.Serialization.Generic;
using System.Collections;

namespace JsonParse.Serialization {
#region DeserializeSaveble data class
	public sealed class DeserializeSaveble : DeserializeNode<ISaveble> {

		private ObjectContainer container = null;

		protected override IEnumerable Numerator() {
			if (this.container == null) yield break;
			ISavebleAsync saveAsync = this.value as ISavebleAsync;
			if(saveAsync != null) {
				AsyncDeserializer deserializer = saveAsync.LoadAsync(this.container);
				while(deserializer.isDone == false)	yield return null;
			} else	this.value.Load(this.container);
			yield break;
		}

		public DeserializeSaveble(ISaveble value, string data) : base(value, string.Empty) {
			this.container = Container.Deserialize<ObjectContainer>(data);
		}

		public DeserializeSaveble(ISaveble value, ObjectContainer container) : base(value, string.Empty) {
			this.container = container;
		}

	}
#endregion
}