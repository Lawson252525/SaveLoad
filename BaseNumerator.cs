using System;
using System.Collections;

namespace JsonParse.Serialization {
#region Base serialize and deserialize node data class
	public abstract class BaseNumerator : IEnumerator {

		public object value { get; protected set;}

		private IEnumerator numerator = null;

		protected abstract IEnumerable Numerator();

		public bool isDone {
			get { return MoveNext() == false;}
		}

		public bool MoveNext() {
			return this.numerator.MoveNext();
		}

		void IEnumerator.Reset() {

		}

		object IEnumerator.Current {
			get { return null;}
		}

		public BaseNumerator(object value) {
			this.value = value;
			this.numerator = Numerator().GetEnumerator();
		}

		public static implicit operator bool(BaseNumerator node) {
			return Equals(node, null) == false;
		}

	}
#endregion
}