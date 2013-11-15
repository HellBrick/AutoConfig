AutoConfig
==========

Description
-----------

.NET application settings library (System.Configuration) is a cool complex thing that provides us with a lot of possibilities. Which is awesome if you actually need all those advanced things, but it turns out to be a huge pain in the ass if you don't.

Most of the time all we need from the app.config is just to read some settings from it in a simple and conveinent manner. And if you've ever tried writing a custom configuration section for that purpose, you'll agree that the default way to do it is everything but simple or conveinent. It requires deriving from special classes, marking properties with attributes and doing a lot of type casting -- well, that's a lot of hassle for such a simple task. The point of AutoConfig is to allow you to skip all these meaningless things and just load configuration sections into your POCO classes.

Nuget package
-------------

[https://www.nuget.org/packages/PocoConfig/](https://www.nuget.org/packages/PocoConfig/)

How do I use it?
----------------

The easiest way to explain this is to demonstrate a simple example. Let's say we want to load a configuration section that's represented by the following class:

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
	
Like always, you need to add a section declaration to your app.config. But the important thing to note here is that no matter what class you're going to load your configuration into, the Type attribute always has the same value "AutoConfig.SectionType, AutoConfig". Then just add the configuration for your section, there's nothing tricky about it.

	<?xml version="1.0" encoding="utf-8" ?>
	<configuration>
		<configSections>
			<section type="AutoConfig.SectionType, AutoConfig" name="SampleConfig"/>
		</configSections>
		
		<SampleConfig threads="32" interval="01:23:45">
			<credentials>
				<login>UserName</login>
				<password>VeryDifficultPassword</password>
			</credentials>
		</SampleConfig>
	</configuration>
	
Now, loading this section is incredibly easy:

	SampleConfig section = AutoConfigManager.GetSection<SampleConfig>();
	
Cool, eh? Configuration section is found using the type name as the section name, and its contents is loaded into your POCO class, just like that. (Though it's not obligatory for the section and the class to have the same name, GetSection<T>() has an overload that allows to pass a name of the section to load).

(You can see the full sample with your own eyes in the [AutoConfig.Sample](https://github.com/HellBrick/AutoConfig/tree/master/AutoConfig.Sample) project.)

Limitations
-----------

1. Class you're loading the configuration into must be a non-abstract class with a public parameterless constructor. The same goes for the types of all its properties (and its properties' properties, etc.), unless they are one of the standard types that .NET configuration framework can just parse from string obviously.

2. Collection types are not supported at the moment.

3. AutoConfig exposes simple read-only functionality, so there's no saving properties to user.config or other advanced features, at least not at the moment.
