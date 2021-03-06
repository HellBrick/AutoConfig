﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfig.Sample
{
	public class SampleConfig
	{
		public int Threads { get; set; }
		public TimeSpan Interval { get; set; }
		public float? SomethingOptional { get; set; }

		public Credentials Credentials { get; set; }
	}

	public class Credentials
	{
		public string Login { get; set; }
		public string Password { get; set; }
	}
}
