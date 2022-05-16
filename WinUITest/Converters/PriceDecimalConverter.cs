using System;
using Microsoft.UI.Xaml.Data;

namespace WinUITest.Converters
{
    public class PriceDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var ret = value.ToString();

            if (!string.IsNullOrEmpty(ret))
            {
                ret = string.Format("{0:0.00}", ret);
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (decimal)value;
        }
    }
}
