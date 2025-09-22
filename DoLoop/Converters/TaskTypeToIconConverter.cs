using System;
using System.Globalization;
using System.Windows.Data;

namespace DoLoop.Converters
{
    public class TaskTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskType taskType)
            {
                return taskType switch
                {
                    TaskType.Important => "A",
                    TaskType.Quick => "O",
                    _ => "V"
                };
            }
            return "V";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
