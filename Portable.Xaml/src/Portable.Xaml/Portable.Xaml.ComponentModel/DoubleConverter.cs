﻿#if PCL
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portable.Xaml.ComponentModel;
using System.Reflection;

namespace Portable.Xaml.ComponentModel
{

	public class DoubleConverter : BaseNumberConverter
	{
		internal override bool AllowHex { get { return false; } }

		internal override Type NumberType { get { return typeof(double); } }

		internal override object FromString(string value, int fromBase)
		{
			return Convert.ToDouble(value, CultureInfo.InvariantCulture);
		}

		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return double.Parse(value, NumberStyles.Float, formatInfo);
		}

		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((double)value).ToString("R", formatInfo);
		}
	}
	
}
#endif