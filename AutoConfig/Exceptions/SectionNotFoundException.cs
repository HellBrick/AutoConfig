using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfig
{
	/// <summary>
	/// The exception that is thrown when an attempt to find a configuration section fails.
	/// </summary>
	[Serializable]
	public class SectionNotFoundException: Exception
	{
		public SectionNotFoundException( string message, string sectionName )
			: base( message )
		{
			SectionName = sectionName;
		}

		public SectionNotFoundException( string message, string sectionName, Exception inner )
			: base( message, inner )
		{
			SectionName = sectionName;
		}

		protected SectionNotFoundException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
			if ( info != null )
				SectionName = info.GetString( _sectionNameKey );
		}

		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData( info, context );

			if ( info != null )
				info.AddValue( _sectionNameKey, SectionName );
		}

		private const string _sectionNameKey = "SectionName";
		public string SectionName { get; private set; }
	}
}
