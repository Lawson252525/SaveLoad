using System;

namespace JsonParse.Serialization.Generic {
#region Generic DeserializeNode data class
	public class DeserializeNode<T> : DeserializeNode {

		public new T value { 
			get { return (T)base.value;}
			protected set { base.value = value;}
		}

		public DeserializeNode(T value, string data) : base(value, data) {}

	}
#endregion
}