using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Application.Events;
using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.Infrastructure.Persistence.Context;
using ArmEquipmentRepair.Infrastructure.Persistence.Model;
using MediatR;

namespace ArmEquipmentRepair.Infrastructure.Implementations.Repository
{
    public class CommentsRepository(MyDbContext db, IMediator mediator, IUser user) : ICommentsRepository
    {
        public Task<CommentEnt> CreateAsync(CommentEnt entity)
        {
            Comment comment = new Comment
            {
                CreatedDate = entity.CreatedDate,
                EmployeeId = entity.EmployeeId,
                LastModify = entity.CreatedDate,
                RequestId = entity.RequestId,
                Text = entity.Text
            };

            db.Comments.Add(comment);
            db.SaveChanges();

            mediator.Publish(new CommentCreatedEvent
            {
                RequestId = entity.RequestId,
                CommentDto = new CommentDto
                {
                    CreatedDate = comment.CreatedDate.Value,
                    CreatorName = comment.Employee.FirstName,
                    IsCreator = comment.EmployeeId == user.Employee.Id,
                    Id = comment.Id,
                    Text = comment.Text
                }
            });


            entity.Id = comment.Id;
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(int entityId)
        {
            var comment = db.Comments.Find(entityId);
            db.Comments.Remove(comment);
            db.SaveChanges();

            mediator.Publish(new CommentDeletedEvent { CommentId = entityId, RequestId = comment.RequestId.Value });

            return Task.CompletedTask;
        }

        public Task<List<CommentDto>> GetCommentsByRequest(int requestId)
        {
            return Task.FromResult(db.Comments.Where(c => c.RequestId == requestId)
                .Select(c => new CommentDto
                {
                    CreatedDate = c.CreatedDate.Value,
                    CreatorName = c.Employee.FirstName,
                    IsCreator = c.EmployeeId == user.Employee.Id,
                    Id = c.Id,
                    Text = c.Text
                }).ToList());
        }

        public Task UpdateAsync(CommentEnt entity)
        {
            throw new NotImplementedException();
        }
    }
}
