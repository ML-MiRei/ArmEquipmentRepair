using ArmEquipmentRepair.Application.Contracts.Repository;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ArmEquipmentRepair.Application.Requests.Commands.UpdateRequest
{
    internal class UpdateRequestHandler(IRequestsRepository requestsRepository) : IRequestHandler<UpdateRequestCommand>
    {
        public Task Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext(request.Request);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(request.Request, context, results))
            {
                ValidationException validationException = new ValidationException();
                throw validationException;
            }

            return requestsRepository.UpdateAsync(request.Request);
        }
    }
}
