using System.ComponentModel.DataAnnotations;

namespace ArmEquipmentRepair.Domain.ValidationAttributes
{
    public class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string email)
            {
                if (email.Contains('@'))
                    return true;
                else
                    ErrorMessage = "Некорректный email";
            }

            return false;
        }
    }
}
