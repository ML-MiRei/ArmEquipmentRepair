using System.ComponentModel.DataAnnotations;

namespace ArmEquipmentRepair.Domain.ValidationAttributes
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            int n;
            if (value.ToString().Length == 11 && int.TryParse(value.ToString(), out n))
                return true;

            ErrorMessage = "Некорректный номер телефона";
            return false;
        }
    }
}
