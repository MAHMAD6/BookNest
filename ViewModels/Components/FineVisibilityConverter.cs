using System.Globalization;

namespace BookNest.ViewModels.Components
{
    public class FineVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string type = parameter as string ?? string.Empty;
            decimal? valueDecimal = (decimal?)value;
            bool result = valueDecimal != null;
            if (type == "ReturnButton")
            {
                result = !result;
            }

            return result;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible;
            }
            return false;
        }
    }
}
