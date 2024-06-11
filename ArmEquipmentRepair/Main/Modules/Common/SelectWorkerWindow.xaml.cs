using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Application.Workers.Queries.GetWorkers;
using MediatR;
using System.Windows;

namespace ArmEquipmentRepair.UI.Main.Modules.Common
{
    public partial class SelectWorkerWindow : Window
    {
        public SelectWorkerWindow(IMediator mediator, WorkerDto selectedWorker = null)
        {
            InitializeComponent();
            DataContext = this;
            SelectedWorker = selectedWorker;
            Workers = mediator.Send(new GetWorkersQuery()).Result;
        }

        public WorkerDto SelectedWorker { get; set; }
        public List<WorkerDto> Workers { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
