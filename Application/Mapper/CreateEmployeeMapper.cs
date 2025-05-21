using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper.Base;
using Domain.Entitites;

namespace Application.Mapper;

public class CreateEmployeeMapper : IBaseMappper<EmployeeResponse, Employee, CreateEmployeeRequest>
{
    public Employee ToEntity(CreateEmployeeRequest input)
    {
        return new()
        {
            Name = input.Name,
            CPF = input.CPF,
            SimultaneousServices = input.SimultaneousServices,
            Email = input.Email,
            CorporateEmail = input.CorporateEmail,
            Phone = input.Phone,
        };
    }

    public EmployeeResponse ToDto(Employee input)
    {
        return new()
        {
            Id = input.Id,
            Name = input.Name,
            CPF = input.CPF,
            SimultaneousServices = input.SimultaneousServices,
            Email = input.Email,
            CorporateEmail = input.CorporateEmail,
            Phone = input.Phone
        };
    }
}