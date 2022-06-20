using System;
using System.Globalization;
using Microsoft.UI.Xaml.Data;


namespace WinUITest.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            // Retrieve the format string and use it to format the value.
            string formatString = parameter as string;
            if (!string.IsNullOrEmpty(formatString))
            {
                return string.Format(
                    new CultureInfo(language), formatString, value);
            }
            // If the format string is null or empty, simply call ToString()
            // on the value.
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new NotImplementedException();
        }
    }
}
