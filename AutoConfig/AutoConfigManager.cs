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
	/// <summary>
	/// Provides routines to load configuration sections as strongly typed POCO objects.
	/// </summary>
	public static class AutoConfigManager
	{
		/// <summary>
		/// Gets the section with the name that matches the name of the class passed as a type parameter.
		/// </summary>
		/// <typeparam name="T">Type of the object to load the section data to. Must be a non-abstract class with parameterless constructor.</typeparam>
		/// <exception cref="AutoConfig.SectionNotFoundException">Failed to find a section with the specified name.</exception>
		/// <exception cref="AutoConfig.TypeConstructionException">Failed ot create an instance of one of the types of the T's properties.</exception>
		/// <exception cref="AutoConfig.PropertyParsingException">Failed to parse one of the class properties from the string found in the configuration file.</exception>
		public static T GetSection<T>() where T: new()
		{
			return GetSection<T>( typeof( T ).Name );
		}

		/// <summary>
		/// Gets the section with the specified name from the configuration.
		/// </summary>
		/// <typeparam name="T">Type of the object to load the section data to. Must be a non-abstract class with parameterless constructor.</typeparam>
		/// <param name="sectionName">Name of the section to find in the configuration file.</param>
		/// <exception cref="AutoConfig.SectionNotFoundException">Failed to find a section with the specified name.</exception>
		/// <exception cref="AutoConfig.TypeConstructionException">Failed ot create an instance of one of the types of the T's properties.</exception>
		/// <exception cref="AutoConfig.PropertyParsingException">Failed to parse one of the class properties from the string found in the configuration file.</exception>
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
