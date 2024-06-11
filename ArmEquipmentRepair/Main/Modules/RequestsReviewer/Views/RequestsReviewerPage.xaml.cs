using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.ViewModels;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views.Templates;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views
{
    public partial class RequestsReviewerPage : Page
    {
        public RequestsReviewerPage(RequestReviewerVM requestReviewerVM)
        {
            InitializeComponent();
            DataContext = requestReviewerVM;
        }

        private void ItemsControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (DataContext as RequestReviewerVM).ShowRequestInfo((sender as RequestTemplate).DataContext as RequestEnt);
        }

        private void RequestTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void RequestTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
