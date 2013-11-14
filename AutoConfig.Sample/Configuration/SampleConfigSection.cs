﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfig.Sample.Configuration
{
	[SettingsProvider( typeof( AutoSettingsProvider ) )]
	public class SampleConfigSection
	{
		public const string Name = "SampleConfigSection";

		public string Login { get; set; }
		public string Password { get; set; }
		public TimeSpan Interval { get; set; }
		public TimeSpan? NullableInterval { get; set; }
	}
}