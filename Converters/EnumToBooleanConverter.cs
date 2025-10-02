using System;
using System.Globalization;
using System.Windows.Data;

namespace DoLoop.Converters
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;
            string parameterString = parameter.ToString();
            if (Enum.IsDefined(value.GetType(), value) == false) return false;
            var paramValue = Enum.Parse(value.GetType(), parameterString);
            return paramValue.Equals(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null) return Binding.DoNothing;
            if ((bool)value)
                return Enum.Parse(targetType, parameter.ToString());
            return Binding.DoNothing;
        }
    }
}
