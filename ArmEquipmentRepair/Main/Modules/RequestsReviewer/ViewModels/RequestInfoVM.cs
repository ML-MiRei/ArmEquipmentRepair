using ArmEquipmentRepair.Application.Comments.Commands.CreateComment;
using ArmEquipmentRepair.Application.Comments.Commands.DeleteComment;
using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Application.Requests.Commands.DeleteRequest;
using ArmEquipmentRepair.Application.Requests.Commands.SetExecutor;
using ArmEquipmentRepair.Application.Requests.Commands.UpdateRequest;
using ArmEquipmentRepair.Application.Requests.Queries.GetInfoByRequest;
using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.Domain.Enums;
using ArmEquipmentRepair.UI.Main.Modules.Common;
using ArmEquipmentRepair.UI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Windows;

namespace ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.ViewModels
{
    public partial class RequestInfoVM : ObservableObject
    {
        private static IUser _user;
        private static IMediator _mediator;


        public RequestInfoVM(IMediator mediator, RequestEnt requestEnt, IUser user, IEventsService eventsService)
        {
            _mediator = mediator;
            _user = user;

            request = mediator.Send(new GetInfoByRequestQuery { RequestId = requestEnt.Id }).Result;

            eventsService.RequestUpdated += OnRequestUpdated;
            eventsService.CommentCreated += OnCommentCreated;
            eventsService.CommentDeleted += OnCommentDeleted;
        }

        private void OnCommentDeleted(object? sender, (int, int) e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (Request.RequestId == e.Item1)
                    Request.Comments.Remove(Request.Comments.First(c => c.Id == e.Item2));
            });
        }

        private void OnCommentCreated(object? sender, (CommentDto, int) e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (request.RequestId == e.Item2)
                    Request.Comments.Add(e.Item1);
            });

        }

        private void OnRequestUpdated(object? sender, RequestEnt e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                OnPropertyChanged(nameof(Request));
                OnPropertyChanged(nameof(Request.Status));
                OnPropertyChanged(nameof(Request.Executor));
            });
        }




        private bool _updateMode = false;
        public bool UpdateMode
        {
            get { return _updateMode; }
            set
            {
                _updateMode = value;

                if (!value)
                {
                    SaveChanges();
                }

                OnPropertyChanged(nameof(IsReadOnly));
                OnPropertyChanged(nameof(UpdateMode));
            }
        }
        public bool IsReadOnly => !UpdateMode;

        [ObservableProperty]
        private RequestFullInfoDto request;

        [ObservableProperty]
        private string commentText;

        [ObservableProperty]
        private List<TypeFaultEnum> typeFaults = Enum.GetValues(typeof(TypeFaultEnum)).Cast<TypeFaultEnum>().ToList();

        public Visibility OperatorsButtonVisibility => (_user.Employee.Role == EmployeeRoleEnum.Operator)
                                                           && request.Status != RequestStatusEnum.Succsess ? Visibility.Visible : Visibility.Collapsed;
        public Visibility UpdateModeCheckBoxVisibility => (_user.Employee.Role == EmployeeRoleEnum.Operator || _user.Employee.Role == EmployeeRoleEnum.Administrator)
                                                            && request.Status != RequestStatusEnum.Succsess ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ExecutorButtonVisibility => (request.Executor != null && _user.Employee.Id == request.Executor.Id)
                                                            && request.Status != RequestStatusEnum.Succsess ? Visibility.Visible : Visibility.Collapsed;





        public void SaveChanges()
        {
            if (MessageBox.Show("Сохранить изменения?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                TaskManager.AddTask(async () =>
                {
                    try
                    {
                        await _mediator.Send(new UpdateRequestCommand
                        {
                            Request = new RequestEnt
                            {
                                Description = request.Description,
                                CreatedDate = request.CreatedDate,
                                Equipment = request.Equipment,
                                Status = request.Status,
                                Id = request.RequestId,
                                TypeFault = request.Type
                            }
                        });
                        MessageBox.Show($"Изменения заявки #{request.RequestId} успешно сохранены");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Ошибка сохранения изменений заявки #{request.RequestId}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        [RelayCommand]
        private void Delete()
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить заявку?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                TaskManager.AddTask(async () =>
                {
                    try
                    {
                        await _mediator.Send(new DeleteRequestCommand { RequestId = request.RequestId });
                        MessageBox.Show("Заявка удалена успешно");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка удаления заявки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        [RelayCommand]
        private void SetExecutor()
        {
            SelectWorkerWindow selectWorkerWindow = new SelectWorkerWindow(_mediator);
            if (selectWorkerWindow.ShowDialog().Value)
            {
                if (MessageBox.Show($"Вы уыерены, что хотите назначить \"{selectWorkerWindow.SelectedWorker.FullName}\" исполнителем заявки #{request.RequestId} ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    TaskManager.AddTask(async () =>
                    {
                        try
                        {
                            await _mediator.Send(new SetExecutorCommand { RequestId = request.RequestId, EmployeeId = selectWorkerWindow.SelectedWorker.Id });
                            MessageBox.Show($"Исполнитель заявки #{request.RequestId} успешно назначен");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show($"Ошибка назначения исполнителя заявки #{request.RequestId}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    });
                }
            }
        }

        [RelayCommand]
        private void SetSuccsessStatus()
        {
            if (MessageBox.Show($"Вы уыерены, что хотите подтвердить выполнение заявки #{request.RequestId} ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                TaskManager.AddTask(async () =>
                {
                    try
                    {
                        await _mediator.Send(new UpdateRequestCommand
                        {
                            Request =
                            new RequestEnt
                            {
                                Description = request.Description,
                                CreatedDate = request.CreatedDate,
                                Equipment = request.Equipment,
                                Id = request.RequestId,
                                Status = RequestStatusEnum.Succsess,
                                TypeFault = request.Type
                            }
                        });
                        MessageBox.Show($"Выполнение заявки успешно подтверждено #{request.RequestId}");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Ошибка подтверждения выполнения заявки #{request.RequestId}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        public void DeleteComment(int commentId)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить комментарий?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                TaskManager.AddTask(async () =>
                {
                    try
                    {
                        await _mediator.Send(new DeleteCommentCommand { CommentId = commentId });
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка удаления комментария", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        [RelayCommand]
        private async void AddComment()
        {
            if (string.IsNullOrEmpty(CommentText))
                return;

            try
            {
                await _mediator.Send(new CreateCommentCommand
                {
                    CommentEnt =
                    new CommentEnt
                    {
                        CreatedDate = DateTime.Now,
                        EmployeeId = _user.Employee.Id,
                        RequestId = request.RequestId,
                        Text = CommentText
                    }
                });
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления комментария", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            CommentText = String.Empty;
        }
    }
}
