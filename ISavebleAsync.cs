using System;
using JsonParse.Serialization;

namespace JsonParse.Serialization {
#region ISavebleAsync interface
	public interface ISavebleAsync : ISaveble {
		AsynсSerializer SaveAsync();
		AsyncDeserializer LoadAsync(ObjectContainer container);
	}
#endregion
}