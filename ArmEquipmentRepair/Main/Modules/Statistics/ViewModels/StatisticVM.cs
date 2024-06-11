using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Application.Requests.Queries.GetStatistic;
using ArmEquipmentRepair.Domain.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;

namespace ArmEquipmentRepair.UI.Main.Modules.Statistics.ViewModels
{
    public partial class StatisticVM : ObservableObject
    {
        private static IMediator _mediator;

        public StatisticVM(IMediator mediator)
        {
            _mediator = mediator;

            SetStatistic();
        }


        private void SetStatistic()
        {
            var requestsDto = _mediator.Send(new GetStatisticQuery()).Result;
            Count = requestsDto.Count;
            CountInProcess = requestsDto.CountInProcess;
            CountSuccsess = requestsDto.CountSuccsess;
            AverageTime = requestsDto.AverageTime;

            CountByTypeFaults = requestsDto.CountByTypeFaults.OrderByDescending(d => d.Value).Select(d => new TypeFaultCountModel
            {
                Procent = d.Value * 100 / count,
                TypeFault = d.Key
            }).ToList();
        }


        [RelayCommand]
        private void UpdateStatistic()
        {
            SetStatistic();
        }


        [ObservableProperty]
        private int count;

        [ObservableProperty]
        private int countInProcess;

        [ObservableProperty]
        private int countSuccsess;

        [ObservableProperty]
        private double averageTime;

        [ObservableProperty]
        private List<TypeFaultCountModel> countByTypeFaults;


        public class TypeFaultCountModel
        {
            public TypeFaultEnum TypeFault { get; set; }
            public int Procent { get; set; }
        }

        public int ProcentCountInProcess => countInProcess * 100 / count;
        public int ProcentCountSuccsess => countSuccsess * 100 / count;

    }
}
