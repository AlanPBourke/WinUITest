using System;
using Microsoft.UI.Xaml.Data;

namespace WinUITest.Converters
{
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return string.Format("{0:0.00}");
            }

            //var ret = value.ToString();

            //if (!string.IsNullOrEmpty(ret))
            //{
            //    ret = string.Format("{0:0.00}", ret);
            //}

            //return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //return (decimal)value;
            Double n;
            bool isNumeric = Double.TryParse(value.ToString(), out n);
            if (isNumeric)
            {
                return n;
            }
            else
            {
                return 0;
            }
        }
    }
}
