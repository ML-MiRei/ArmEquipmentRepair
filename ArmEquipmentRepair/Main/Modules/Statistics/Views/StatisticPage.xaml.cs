using ArmEquipmentRepair.UI.Main.Modules.Statistics.ViewModels;
using System.Windows.Controls;

namespace ArmEquipmentRepair.UI.Main.Modules.Statistics.Views
{
    /// <summary>
    /// Логика взаимодействия для StatisticPage.xaml
    /// </summary>
    public partial class StatisticPage : Page
    {
        public StatisticPage(StatisticVM statisticVM)
        {
            InitializeComponent();
            DataContext = statisticVM;
        }
    }
}
