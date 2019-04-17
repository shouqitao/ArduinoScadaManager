using System;
using System.Globalization;
using System.Windows.Data;

namespace ArduinoScadaManager.Common.Converters {
    [ValueConversion(typeof(Enum), typeof(bool))]
    public class EnumToBoolConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var inverse = false;

            if (value == null || parameter == null) return false;
            string enumValue = value.ToString();
            string targetValue = parameter.ToString();
            if (targetValue.StartsWith("!")) {
                inverse = true;
                targetValue = targetValue.Substring(1);
            }

            bool outputValue = enumValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase);
            return inverse ? !outputValue : outputValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || parameter == null) return null;
            var useValue = (bool) value;
            string targetValue = parameter.ToString();
            if (useValue) return Enum.Parse(targetType, targetValue);
            return null;
        }
    }
}