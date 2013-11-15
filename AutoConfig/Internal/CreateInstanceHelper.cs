using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfig.Internal
{
	internal static class InstanceCreator
	{
		private const string _errorFormat =
			"Failed to create an instance of the type '{0}'. " +
			"The type must be a non-abstract class with a public parameterless constructor.";

		public static object Create( Type type )
		{
			try
			{
				return Activator.CreateInstance( type );
			}
			catch ( MissingMethodException ex )
			{
				throw new TypeConstructionException( String.Format( _errorFormat, type ), type, ex );
			}
		}
	}
}
