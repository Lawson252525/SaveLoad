using System;
using System.Collections;
using JsonParse.Serialization.Generic;

namespace JsonParse.Serialization {
#region SerailizeConvert data class
	public sealed class SerailizeConvert : SerializeNode<IConvertible> {

		protected override IEnumerable Numerator() {
			this.result = this.value.ToString();
			yield break;
		}

		public SerailizeConvert(string key, IConvertible value) : base(key, value) {}

	}
#endregion
}