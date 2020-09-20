using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DescontosEmPortugal.Core.Helpers
{
	public static class ConvertToDouble
	{
		public static double StringPriceToDouble(string value)
		{
			double result;

			// Try parsing in the current culture
			if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("DE-de"), out result) &&
				// Then try in US english
				!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
				// Then in neutral language
				!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
			{
				result = 0;
			}
			return result;
		}
	}
}