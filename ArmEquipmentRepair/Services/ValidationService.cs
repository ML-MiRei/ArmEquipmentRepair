using ArmEquipmentRepair.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace ArmEquipmentRepair.UI.Services
{
    class ValidationService
    {
        public static bool IsValide<T>(T t) where T : BaseEntity
        {
            var context = new ValidationContext(t);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(t, context, results))
            {
                string messages = string.Join('\n', results.Select(r => r.ErrorMessage));
                MessageBox.Show("Данные  введены неверно\n" + messages, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

    }
}
