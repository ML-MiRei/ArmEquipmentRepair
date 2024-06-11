using ArmEquipmentRepair.UI.Main.Modules.RequestCreater.ViewModel;
using System.Windows.Controls;

namespace ArmEquipmentRepair.UI.Main.Modules.RequestCreater.View
{
    /// <summary>
    /// Логика взаимодействия для RequestCreaterPage.xaml
    /// </summary>
    public partial class RequestCreaterPage : Page
    {
        public RequestCreaterPage(RequestCreaterPageVM requestCreaterPageVM)
        {
            InitializeComponent();
            DataContext = requestCreaterPageVM;
        }
    }
}
