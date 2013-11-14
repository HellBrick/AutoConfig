using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfig
{
	public class AutoConfigSection : IConfigurationSectionHandler
	{
		#region IConfigurationSectionHandler Members

		public object Create( object parent, object configContext, System.Xml.XmlNode section )
		{
			return section;
		}

		#endregion
	}
}
