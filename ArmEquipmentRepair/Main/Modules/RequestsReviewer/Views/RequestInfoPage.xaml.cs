using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.ViewModels;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views.Templates;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views
{
    public partial class RequestInfoPage : Page
    {
        private static RequestInfoVM _requestInfoVM;


        public RequestInfoPage(RequestInfoVM requestInfoVM)
        {
            InitializeComponent();
            DataContext = requestInfoVM;

            _requestInfoVM = requestInfoVM;
        }

        private void CommentTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((sender as CommentTemplate).DataContext as CommentDto).IsCreator)
                Mouse.OverrideCursor = Cursors.Hand;
        }

        private void CommentTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void CommentTemplate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((sender as CommentTemplate).DataContext as CommentDto).IsCreator)
                _requestInfoVM.DeleteComment(((sender as CommentTemplate).DataContext as CommentDto).Id);
        }
    }
}
