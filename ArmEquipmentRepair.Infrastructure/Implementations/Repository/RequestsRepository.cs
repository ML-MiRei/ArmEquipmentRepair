using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Application.Events;
using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.Domain.Enums;
using ArmEquipmentRepair.Infrastructure.Persistence.Context;
using ArmEquipmentRepair.Infrastructure.Persistence.Model;
using MediatR;

namespace ArmEquipmentRepair.Infrastructure.Implementations.Repository
{
    public class RequestsRepository(MyDbContext db, IMediator mediator, INotificationsRepository notificationsRepository, IUser user) : IRequestsRepository
    {
        public Task ChangeStatus(int requestId, RequestStatusEnum status)
        {
            var request = db.Requests.Find(requestId);

            request.StatusId = (int)status;

            db.Requests.Update(request);
            db.SaveChanges();

            notificationsRepository.CreateNoification($"У заявки #{request.Id} изменился статус", db.Employees.Where(e => e.RoleId == (int)EmployeeRoleEnum.Operator)
                                                                                                   .Select(e => e.Id).ToArray());
            mediator.Publish(new RequestUpdatedEvent
           (new RequestEnt
           {
               Description = request.Description,
               CreatedDate = request.CreatedDate.Value,
               Equipment = request.Equipment,
               Id = request.Id,
               Status = (RequestStatusEnum)request.StatusId,
               TypeFault = (TypeFaultEnum)request.TypeId
           }));
            return Task.CompletedTask;
        }

        public Task<RequestEnt> CreateAsync(RequestEnt entity, ClientEnt clientEnt)
        {
            Client client = new Client
            {
                CreatedDate = DateTime.Now,
                LastModify = DateTime.Now,
                Email = clientEnt.Email,
                PhoneNumber = clientEnt.PhoneNumber,
                FirstName = clientEnt.FirstName,
                LastName = clientEnt.LastName,
                SecondName = clientEnt.SecondName
            };

            db.Clients.Add(client);
            db.SaveChanges();

            Request request = new Request
            {
                Description = entity.Description,
                CreatedDate = entity.CreatedDate,
                TypeId = (int)entity.TypeFault,
                StatusId = (int)entity.Status,
                ClientId = client.Id,
                LastModify = entity.CreatedDate,
                Equipment = entity.Equipment,
            };

            db.Requests.Add(request);
            db.SaveChanges();

            entity.Id = request.Id;

            notificationsRepository.CreateNoification($"Добавлена заявка #{request.Id}", db.Employees.Where(e => e.RoleId == (int)EmployeeRoleEnum.Operator)
                                                                                                     .Select(e => e.Id).ToArray());

            mediator.Publish(new RequestCreatedEvent(entity));
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(int entityId)
        {
            var request = db.Requests.Find(entityId);
            db.Requests.Remove(request);
            db.SaveChanges();

            notificationsRepository.CreateNoification($"Удалена заявка #{entityId}", db.Employees.Where(e => e.RoleId == (int)EmployeeRoleEnum.Operator)
                                                                                              .Select(e => e.Id).ToArray());

            mediator.Publish(new RequestDeletedEvent(entityId));
            return Task.CompletedTask;
        }

        public Task<IEnumerable<RequestEnt>> GetAllAsync()
        {
            return Task.FromResult((IEnumerable<RequestEnt>)db.Requests.Select(r => new RequestEnt
            {
                CreatedDate = r.CreatedDate.Value,
                Description = r.Description,
                Equipment = r.Equipment,
                Id = r.Id,
                Status = (RequestStatusEnum)r.StatusId,
                TypeFault = (TypeFaultEnum)r.TypeId
            }));
        }


        public Task<RequestFullInfoDto> GetInfoByRequestId(int id)
        {
            var request = db.Requests.Find(id);

            RequestFullInfoDto requestInfo = new RequestFullInfoDto
            {
                Description = request.Description,
                Equipment = request.Equipment,
                CreatedDate = request.CreatedDate.Value,
                Client = db.Clients.Where(c => c.Id == request.ClientId).Select(c => new PeopleDTO
                {
                    FormatedPhoneNumber = c.PhoneNumber,
                    FullName = c.FirstName + " " + c.SecondName + " " + c.LastName,
                    Id = c.Id,
                    Email = c.Email
                }).First(),
                Status = (RequestStatusEnum)request.StatusId,
                Type = (TypeFaultEnum)request.TypeId,
                Comments = new System.Collections.ObjectModel.ObservableCollection<CommentDto>(db.Comments.Where(c => c.RequestId == request.Id).Select(c => new CommentDto
                {
                    CreatedDate = c.CreatedDate.Value,
                    IsCreator = c.EmployeeId == user.Employee.Id,
                    CreatorName = db.Employees.First(e => e.Id == c.EmployeeId).LastName,
                    Text = c.Text,
                    Id = c.Id
                }).ToList()),
                Executor = request.EmployeeId == null ? null : db.Employees.Where(c => c.Id == request.EmployeeId).Select(c => new PeopleDTO
                {
                    FormatedPhoneNumber = c.PhoneNumber,
                    FullName = c.FirstName + " " + c.SecondName + " " + c.LastName,
                    Id = c.Id
                }).First(),
                RequestId = id,
            };

            return Task.FromResult(requestInfo);
        }

        public Task<IEnumerable<RequestEnt>> GetRequestsByFilters(FiltersDto filters)
        {

            var requests = db.Requests.ToList();

            if (filters.Status != null)
                requests = requests.Where(r => (RequestStatusEnum)r.StatusId == filters.Status).ToList();

            if (filters.SearchingEquipment != null)
                requests = requests.Where(r => r.Equipment.ToLower().Contains(filters.SearchingEquipment.ToLower())).ToList();

            if (filters.TypeFault != null)
                requests = requests.Where(r => (TypeFaultEnum)r.TypeId == filters.TypeFault).ToList();

            if (filters.ClientLastName != null)
                requests = requests.Where(r => db.Clients.First(c => c.Id == r.ClientId).LastName.ToLower().Contains(filters.ClientLastName.ToLower())).ToList();

            return Task.FromResult(requests.Select(r => new RequestEnt
            {
                CreatedDate = r.CreatedDate.Value,
                Description = r.Description,
                Equipment = r.Equipment,
                Id = r.Id,
                Status = (RequestStatusEnum)r.StatusId,
                TypeFault = (TypeFaultEnum)r.TypeId
            }));
        }

        public Task<StatisticRequestsDto> GetStatistic()
        {
            StatisticRequestsDto statisticRequestsDto = new StatisticRequestsDto();
            statisticRequestsDto.Count = db.Requests.Count();
            statisticRequestsDto.CountSuccsess = db.Requests.Where(r => r.StatusId == (int)RequestStatusEnum.Succsess).Count();
            statisticRequestsDto.CountInProcess = db.Requests.Where(r => r.StatusId == (int)RequestStatusEnum.InProcess).Count();
            statisticRequestsDto.AverageTime = db.Requests.Where(r => r.StatusId == (int)RequestStatusEnum.Succsess).Average(r => r.LastModify.Value.Day - r.CreatedDate.Value.Day);

            foreach (var type in Enum.GetValues(typeof(TypeFaultEnum)).Cast<int>())
            {
                statisticRequestsDto.CountByTypeFaults[(TypeFaultEnum)type] = db.Requests.Where(r => r.TypeId == type).Count();
            }

            return Task.FromResult(statisticRequestsDto);
        }

        public Task SetExecutor(int requestId, int employeeId)
        {
            var request = db.Requests.Find(requestId);

            request.StatusId = (int)RequestStatusEnum.InProcess;
            request.EmployeeId = employeeId;


            db.Requests.Update(request);
            db.SaveChanges();

            notificationsRepository.CreateNoification($"Вас назначили исполнителем заявки #{request.Id}", employeeId);

            mediator.Publish(new RequestUpdatedEvent
            (new RequestEnt
            {
                Description = request.Description,
                CreatedDate = request.CreatedDate.Value,
                Equipment = request.Equipment,
                Id = request.Id,
                Status = (RequestStatusEnum)request.StatusId,
                TypeFault = (TypeFaultEnum)request.TypeId
            }));
            return Task.CompletedTask;
        }

        public Task UpdateAsync(RequestEnt entity)
        {
            var request = db.Requests.Find(entity.Id);

            request.Description = entity.Description;
            request.Equipment = entity.Equipment;
            request.LastModify = DateTime.Now;
            request.TypeId = (int)entity.TypeFault;
            request.StatusId = (int)entity.Status;

            db.Requests.Update(request);
            db.SaveChanges();

            notificationsRepository.CreateNoification($"Обновлена заявка #{request.Id}", db.Employees.Where(e => e.RoleId == (int)EmployeeRoleEnum.Operator)
                                                                                                   .Select(e => e.Id).ToArray());

            mediator.Publish(new RequestUpdatedEvent(entity));
            return Task.CompletedTask;
        }

    }
}
