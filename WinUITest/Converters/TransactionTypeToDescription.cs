using System;
using Microsoft.UI.Xaml.Data;

namespace WinUITest.Converters;

public class TransactionTypeToDescriptionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var t = value as string;
        if (t == null)
        {
            return string.Empty;
        }
        else
        {
            return t == "I" ? "Invoice" : "Credit";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
