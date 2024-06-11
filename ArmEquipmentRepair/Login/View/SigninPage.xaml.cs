using ArmEquipmentRepair.UI.Login.ViewModel;
using System.Windows.Controls;

namespace ArmEquipmentRepair.UI.Login.View
{
    public partial class SigninPage : Page
    {
        public SigninPage(SigninPageVM signinPageVM)
        {
            InitializeComponent();
            DataContext = signinPageVM;
        }

    }
}
