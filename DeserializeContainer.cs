using System;
using JsonParse.Serialization.Generic;
using System.Collections;

namespace JsonParse.Serialization {
#region DeserializeContainer data class
	public sealed class DeserializeContainer : DeserializeNode<IContainer> {

		protected override IEnumerable Numerator() {
			this.value = Container.Deserialize(this.data, this.valueType);
			yield break;
		}

		public DeserializeContainer(IContainer container, string data) : base(container, data) {}

	}
#endregion
}