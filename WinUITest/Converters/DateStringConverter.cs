using System;
using System.Diagnostics;
using Microsoft.UI.Xaml.Data;

namespace WinUITest.Converters;

public class DateStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        Debug.WriteLine($"{value.ToString()}");
        if (value == null) return null;
        var dt = (DateTime)value;
        return dt.ToShortDateString();
    }


    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}