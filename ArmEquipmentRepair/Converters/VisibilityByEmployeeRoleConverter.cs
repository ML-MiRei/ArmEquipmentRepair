using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ArmEquipmentRepair.UI.Converters
{
    public class VisibilityByEmployeeRoleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value == parameter)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
