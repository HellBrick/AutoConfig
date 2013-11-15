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
			return GetSection<T>( typeof( T ).Name );
		}

		public static T GetSection<T>( string sectionName ) where T: new()
		{
			T section = new T();

			var xmlNode = ConfigurationManager.GetSection( sectionName ) as XmlNode;
			if ( xmlNode == null )
				throw new SectionNotFoundException( String.Format( "Failed to find section <{0}> in the configuration file.", sectionName ), sectionName );

			ClassFiller filler = new ClassFiller( typeof( T ) );
			filler.Fill( section, xmlNode );
			return section;	
		}
	}
}
