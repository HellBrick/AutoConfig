﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoConfig.Sample.Configuration;

namespace AutoConfig.Sample
{
	class Program
	{
		static void Main( string[] args )
		{
			var section = AutoConfigManager.GetSection<SampleConfigSection>( SampleConfigSection.Name );
		}
	}
}
