using System;

namespace JsonParse.Serialization {
#region ISaveble interface
	public interface ISaveble {
		string key { get; set;}
		ObjectContainer Save();
		void Load(ObjectContainer container);
	}
#endregion
}