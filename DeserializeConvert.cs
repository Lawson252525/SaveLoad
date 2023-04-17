using System;
using JsonParse.Serialization.Generic;
using System.Collections;

namespace JsonParse.Serialization {
#region DeserializeConvert data class
	public sealed class DeserializeConvert : DeserializeNode<IConvertible> {

		protected override IEnumerable Numerator() {
			this.value = (IConvertible)Convert.ChangeType(this.data, this.valueType);
			yield break;
		}

		public DeserializeConvert(IConvertible value, string data) : base(value, data) {}

	}
#endregion
}