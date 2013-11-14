using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfig.Sample.Configuration
{
	public class SampleConfig
	{
		public int Threads { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public TimeSpan Interval { get; set; }
		public TimeSpan? NullableInterval { get; set; }
	}
}
