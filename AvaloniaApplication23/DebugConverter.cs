using System;
using System.Diagnostics;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AvaloniaApplication23;

public class DebugConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Debug.Write(value);
        return BindingOperations.DoNothing;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Debug.Write(value);
        return BindingOperations.DoNothing;
    }
}