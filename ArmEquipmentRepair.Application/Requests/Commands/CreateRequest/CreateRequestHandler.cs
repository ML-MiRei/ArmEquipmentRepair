using ArmEquipmentRepair.Application.Contracts.Repository;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ArmEquipmentRepair.Application.Requests.Commands.CreateRequest
{
    internal class CreateRequestHandler(IRequestsRepository requestsRepository) : IRequestHandler<CreateRequestCommand>
    {
        Task IRequestHandler<CreateRequestCommand>.Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext(request.Request);

            Validator.ValidateObject(request.Request, context);
            return requestsRepository.CreateAsync(request.Request, request.Client);
        }
    }
}
