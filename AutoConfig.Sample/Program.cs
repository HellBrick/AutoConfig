using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfig.Sample
{
	class Program
	{
		static void Main( string[] args )
		{
			SampleConfig section = AutoConfigManager.GetSection<SampleConfig>();
		}
	}
}
