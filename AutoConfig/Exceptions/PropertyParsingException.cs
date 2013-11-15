using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfig
{
	/// <summary>
	/// Exception that is thrown when a value of a property can't be parsed from a configuration string.
	/// </summary>
	[Serializable]
	public class PropertyParsingException: Exception
	{
		public PropertyParsingException( PropertyInfo type, string valueString, string message )
			: base( message )
		{
			Property = type;
			ValueString = valueString;
		}

		public PropertyParsingException( PropertyInfo type, string valueString, string message, Exception inner )
			: base( message, inner )
		{
			Property = type;
			ValueString = valueString;
		}

		protected PropertyParsingException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
			if ( info != null )
			{
				Property = info.GetValue( _propertyKey, typeof( Type ) ) as PropertyInfo;
				ValueString = info.GetString( _valueStringKey );
			}
		}

		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData( info, context );

			if ( info != null )
			{
				info.AddValue( _propertyKey, Property, typeof( Type ) );
				info.AddValue( _valueStringKey, ValueString, typeof( string ) );
			}
		}

		private const string _propertyKey = "Property";
		public PropertyInfo Property { get; private set; }

		private const string _valueStringKey = "ValueString";
		public string ValueString { get; private set; }
	}
}
