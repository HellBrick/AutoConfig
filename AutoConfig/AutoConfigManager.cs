using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AutoConfig.Internal;

namespace AutoConfig
{
	public static class AutoConfigManager
	{
		public static T GetSection<T>() where T: new()
		{
			return GetSection<T>( typeof( T ).Name, () => new T() );
		}

		public static T GetSection<T>( string sectionName ) where T: new()
		{
			return GetSection<T>( sectionName, () => new T() );			
		}

		public static T GetSection<T>( string sectionName, Func<T> sectionFactory )
		{
			T section = sectionFactory();

			var xmlNode = ConfigurationManager.GetSection( sectionName ) as XmlNode;
			if ( xmlNode == null )
				throw new InvalidOperationException();

			ClassFiller filler = new ClassFiller( typeof( T ) );
			filler.Fill( section, xmlNode );
			return section;
		}
	}
}
