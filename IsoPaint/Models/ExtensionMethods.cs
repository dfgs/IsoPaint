using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsoPaint.Models
{
	public static class ExtensionMethods
	{
		public static string ToPOVString(this float Value)
		{
			return Value.ToString(CultureInfo.InvariantCulture);
		}

		public static string ToPOVColor(this Color Value)
		{
			return "<" + (Value.R / 255.0f).ToPOVString() + "," + (Value.G / 255.0f).ToPOVString() + "," + (Value.B / 255.0f).ToPOVString() + "," + (1.0f-Value.A / 255.0f).ToPOVString() + ">";
		}


	}
}
