using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;
using System.ComponentModel;

namespace AutoConfig.Internal
{
	internal class ClassFiller
	{
		private const BindingFlags _flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		private Type _type;
		private Dictionary<string, PropertyInfo> _properties;

		public ClassFiller( Type type )
		{
			_type = type;
			_properties = _type.GetProperties( _flags ).ToDictionary( p => p.Name.ToUpperInvariant() );
		}

		public void Fill( object obj, XmlNode xmlNode )
		{
			if ( Object.ReferenceEquals( obj, null ) )
				throw new ArgumentNullException( "obj" );

			if ( !_type.IsAssignableFrom( obj.GetType() ) )
				throw new ArgumentException( String.Format( "Type derived from {0} expected; got {1} insted.", _type, obj.GetType(), "obj" ) );

			var attributes = xmlNode.Attributes.Cast<XmlNode>();
			var subnodes = xmlNode.ChildNodes.Cast<XmlNode>();
			var allNodes = Enumerable.Concat( attributes, subnodes );

			foreach ( var node in allNodes )
			{
				PropertyInfo property;
				if ( _properties.TryGetValue( node.Name.ToUpperInvariant(), out property ) )
					FillProperty( obj, property, node );
			}
		}

		private void FillProperty( object obj, PropertyInfo property, XmlNode node )
		{
			Type propertyType = property.PropertyType;
			Object propertyValue = null;

			var converter = TypeDescriptor.GetConverter( propertyType );
			if ( converter.CanConvertFrom( typeof( string ) ) )
			{
				string valueString = node.InnerText;
				propertyValue = ParseProperty( property, valueString, converter );
			}
			else
			{
				propertyValue = InstanceCreator.Create( propertyType );
				var filler = new ClassFiller( propertyType );
				filler.Fill( propertyValue, node );
			}

			if ( propertyValue != null )
				property.SetValue( obj, propertyValue, null );
		}

		private const string _parsingErrorFormat = "Value of the {0}.{1} [{2}] can't be parsed from the string \"{3}\"";

		private static object ParseProperty( PropertyInfo property, string valueString, TypeConverter converter )
		{
			try
			{
				return converter.ConvertFromString( valueString );
			}
			catch ( Exception ex )
			{
				throw new PropertyParsingException(
					property,
					valueString,
					String.Format( _parsingErrorFormat, property.DeclaringType.Name, property.Name, property.PropertyType.Name, valueString ), ex );
			}
		}
	}
}
