using System;
using System.Collections;
using JsonParse.Serialization.Generic;

namespace JsonParse.Serialization {
#region SerializeContainer data class
	public sealed class SerializeContainer : SerializeNode<IContainer> {

		protected override IEnumerable Numerator() {
			this.result = this.value.Serialize();
			yield break;
		}

		public SerializeContainer(string key, IContainer value) : base(key, value) {}

	}
#endregion
}