using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace AvaloniaApplication23;

internal class IntentToColorConverter : IMultiValueConverter
{
    public static IntentToColorConverter Instance { get; } = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        var obj = values.Skip(0).First();
        var rows = values.Skip(1).First() as IRows;

        if (obj is null || rows is null)
        {
            return BindingOperations.DoNothing;
        }

        var rowIndex = rows.FindIndex(el => el.Model == obj);
        var modelIndex = rows?.RowIndexToModelIndex(rowIndex);
        return modelIndex?.Count() switch
        {
            1 => new ImmutableSolidColorBrush(Colors.Red),
            2 => new ImmutableSolidColorBrush(Colors.Green),
            3 => new ImmutableSolidColorBrush(Colors.Blue),
            4 => new ImmutableSolidColorBrush(Colors.Yellow),
            _ => null
        };
    }
}

internal static class ListExtensions
{
    public static int FindIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        int i = 0;
        foreach (var item in source)
        {
            if (predicate(item))
                return i;
            i++;
        }
        return -1;
    }
}