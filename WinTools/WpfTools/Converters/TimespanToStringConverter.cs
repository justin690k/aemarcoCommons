﻿using Extensions.netExtensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WinTools.WpfTools.Converters
{

    [ValueConversion(typeof(TimeSpan?), typeof(string))]
    public class TimespanToStringConverter : IValueConverter
    {
        /// <summary>
        /// One-way converter from TimeSpan? to string 
        /// </summary>


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan? val = (TimeSpan?)value;
            if (val.HasValue)
                return val.Value.ToNiceTimespanString(2, "None");
            else
                return "None";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
