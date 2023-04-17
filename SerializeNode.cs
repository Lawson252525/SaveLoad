using System;
using System.Collections;
using JsonParse.Serialization;

namespace JsonParse.Serialization.Generic {
#region Generic SerializeNode data class
	public class SerializeNode<T> : SerializeNode {

		public new T value {
			get { return (T)base.value;}
			protected set { base.value = value;}
		}

		public SerializeNode(string key, T value) : base(key, value) {}

	}
#endregion
}