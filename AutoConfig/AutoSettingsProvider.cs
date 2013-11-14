using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfig
{
	public class AutoSettingsProvider: SettingsProvider
	{
		public override string ApplicationName
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public override SettingsPropertyValueCollection GetPropertyValues( SettingsContext context, SettingsPropertyCollection collection )
		{
			throw new NotImplementedException();
		}

		public override void SetPropertyValues( SettingsContext context, SettingsPropertyValueCollection collection )
		{
			throw new NotImplementedException();
		}
	}
}
