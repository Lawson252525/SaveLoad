using System;
using JsonParse.Serialization.Generic;
using System.Collections;

namespace JsonParse.Serialization {
#region DeserializeEnum data class
	public sealed class DeserializeEnum : DeserializeNode<Enum> {

		protected override IEnumerable Numerator() {
			this.value = (Enum)Enum.Parse(this.valueType, this.data);
			yield break;
		}

		public DeserializeEnum(Enum value, string data) : base(value, data) {}

	}
#endregion
}