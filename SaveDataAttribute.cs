using System;

namespace JsonParse.Serialization {
#region Save data attribute
	[AttributeUsage(AttributeTargets.Field, Inherited =  true, AllowMultiple = false)]
	public sealed class SaveDataAttribute : Attribute {}
#endregion
}