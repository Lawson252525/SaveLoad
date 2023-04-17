using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace JsonParse.Serialization {
#region AsyncBase base data class
	public abstract class AsyncBase : BaseNumerator, IDisposable {

		public readonly static Type SaveType = typeof(ISaveble);
		public readonly static Type CollectionType = typeof(ICollection);
		public readonly static Type ContainerType = typeof(IContainer);
		public readonly static Type ConvertType = typeof(IConvertible);

		protected readonly static	BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
		protected readonly static Type AttributeType = typeof(SaveDataAttribute);

		protected ObjectContainer container = null;

		public AsyncBase(ISaveble saveObject) : base(saveObject) {}

		protected ISaveble saveObject {
			get { return (ISaveble)this.value;}
		}

		public void Dispose() {
			this.value = null;
			this.container = null;
		}

	}
#endregion
}