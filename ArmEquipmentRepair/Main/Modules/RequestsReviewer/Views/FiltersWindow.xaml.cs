using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Domain.Enums;
using System.Windows;

namespace ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views
{
    public partial class FiltersWindow : Window
    {
        public FiltersDto FiltersDto { get; set; }
        public List<TypeFaultEnum> TypeFaults { get; } = Enum.GetValues(typeof(TypeFaultEnum)).Cast<TypeFaultEnum>().ToList();
        public List<RequestStatusEnum> Statuses { get; } = Enum.GetValues(typeof(RequestStatusEnum)).Cast<RequestStatusEnum>().ToList();

        public FiltersWindow(FiltersDto filtersDto)
        {
            InitializeComponent();
            DataContext = this;
            FiltersDto = filtersDto;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ClearFilters(object sender, RoutedEventArgs e)
        {
            FiltersDto.Clear();
            DialogResult = true;

        }

        private void ApplyFilters(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
