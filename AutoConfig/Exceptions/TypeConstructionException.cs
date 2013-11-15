using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfig
{
	/// <summary>
	/// Exception that is thrown when an attempt to create an instance of a type fails.
	/// </summary>
	[Serializable]
	public class TypeConstructionException: Exception
	{
		public TypeConstructionException( string message, Type type )
			: base( message )
		{
			Type = type;
		}

		public TypeConstructionException( string message, Type type, Exception inner )
			: base( message, inner )
		{
			Type = type;
		}

		protected TypeConstructionException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
			if ( info != null )
				Type = info.GetValue( _typeKey, typeof( Type ) ) as Type;
		}

		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData( info, context );

			if ( info != null )
				info.AddValue( _typeKey, Type, typeof( Type ) );
		}

		private const string _typeKey = "Type";
		public Type Type { get; private set; }
	}
}
