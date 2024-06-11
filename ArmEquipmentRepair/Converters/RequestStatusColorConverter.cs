using ArmEquipmentRepair.Domain.Enums;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ArmEquipmentRepair.UI.Converters
{
    public class RequestStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            switch ((RequestStatusEnum)value)
            {
                case RequestStatusEnum.Waiting:
                    return Brushes.White;
                case RequestStatusEnum.InProcess:
                    return Brushes.LightYellow;
                case RequestStatusEnum.Succsess:
                    return Brushes.LightGreen;
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
