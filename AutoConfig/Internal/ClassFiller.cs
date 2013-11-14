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

			foreach ( var node in xmlNode.ChildNodes.OfType<XmlElement>() )
			{
				PropertyInfo property;
				if ( _properties.TryGetValue( node.Name.ToUpperInvariant(), out property ) )
					FillProperty( obj, property, node );
			}
		}

		private void FillProperty( object obj, PropertyInfo property, XmlElement node )
		{
			Type propertyType = property.PropertyType;
			var configurationProperty = new ConfigurationProperty( node.Name, propertyType );
			var value = configurationProperty.Converter.ConvertFromString( node.InnerText );
			if ( value != null )
				property.SetValue( obj, value );
		}
	}
}
