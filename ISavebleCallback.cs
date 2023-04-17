using System;

namespace JsonParse.Serialization {
#region ISavebleCallback intarface
	public interface ISavebleCallback : ISaveble {
		void OnBeforeSerialize();
		void OnAfterDeserialize();
	}
#endregion
}