using DAM2_DI_UD03_P01_Patrick_Galan.Models;
using System.Globalization;
using System.Windows.Data;

namespace DAM2_DI_UD03_P01_Patrick_Galan.Converters
{
    internal class PrioridadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is true && parameter != null)
                return Enum.Parse(typeof(NivelPrioridad), parameter.ToString()!);

            return Binding.DoNothing;
        }
    }
}
