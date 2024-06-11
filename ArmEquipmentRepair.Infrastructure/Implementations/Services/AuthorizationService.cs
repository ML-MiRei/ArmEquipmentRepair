using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.Domain.Enums;
using ArmEquipmentRepair.Domain.Exceptions;
using ArmEquipmentRepair.Infrastructure.Persistence.Context;
using ArmEquipmentRepair.Infrastructure.Persistence.Model;
using MediatR;

namespace ArmEquipmentRepair.Infrastructure.Implementations.Services;

public class AuthorizationService(MyDbContext db, IMediator mediator, IUser user) : IAuthorizationService
{
    public Task<EmployeeEnt> AuthorizeAsync(LoginDto loginDto)
    {
        var employee = db.Employees.FirstOrDefault(e => e.Login == loginDto.Login);

        if (employee == default)
            throw new LoginNotExistException();

        if (employee.Password != loginDto.Password)
            throw new AuthorizeException();

        user.Employee = new EmployeeEnt
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            SecondName = employee.SecondName,
            LastName = employee.LastName,
            Password = loginDto.Password,
            Login = loginDto.Login,
            PhoneNumber = employee.PhoneNumber,
            Role = (EmployeeRoleEnum)employee.RoleId
        };

        return Task.FromResult(user.Employee);
    }

    public Task<EmployeeEnt> RegisterAsync(EmployeeEnt employeeEntity)
    {
        if (db.Employees.Any(e => e.Login == employeeEntity.Login))
            throw new LoginExistException();

        Employee employee = new Employee
        {
            CreatedDate = DateTime.Now,
            LastModify = DateTime.Now,
            FirstName = employeeEntity.FirstName,
            SecondName = employeeEntity.SecondName,
            LastName = employeeEntity.LastName,
            PhoneNumber = employeeEntity.PhoneNumber,
            Login = employeeEntity.Login,
            Password = employeeEntity.Password,
            RoleId = (int)employeeEntity.Role
        };

        db.Employees.Add(employee);
        db.SaveChanges();

        employeeEntity.Id = employee.Id;

        user.Employee = employeeEntity;

        return Task.FromResult(employeeEntity);
    }

}

