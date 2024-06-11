using ArmEquipmentRepair.UI.Login.ViewModel;
using System.Windows.Controls;

namespace ArmEquipmentRepair.UI.Login.View
{
    public partial class SignupPage : Page
    {
        public SignupPage(SignupPageVM signupPageVM)
        {
            InitializeComponent();
            DataContext = signupPageVM;
        }

    }
}
