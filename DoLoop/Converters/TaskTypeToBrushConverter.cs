using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using DoLoop.Models;

namespace DoLoop.Converters
{
    public class TaskTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TaskType taskType)) return (Brush)Application.Current.Resources["NormalTask"];
            switch (taskType)
            {
                case TaskType.Important: return (Brush)Application.Current.Resources["ImportantTask"];
                case TaskType.Quick: return (Brush)Application.Current.Resources["QuickTask"];
                default: return (Brush)Application.Current.Resources["NormalTask"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
