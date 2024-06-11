using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views.Templates
{
    /// <summary>
    /// Логика взаимодействия для RequestTemplate.xaml
    /// </summary>
    public partial class RequestTemplate : UserControl
    {
        public RequestTemplate()
        {
            InitializeComponent();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
    }
}
