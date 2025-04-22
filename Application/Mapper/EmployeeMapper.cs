using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper.Base;
using Domain.Entitites;

namespace Application.Mapper;

public class EmployeeMapper : IBaseMappper<EmployeeResponse, Employee, EmployeeRequest>
{
    public Employee ToEntity(EmployeeRequest input)
    {
        return new()
        {
            Name = input.Name,
            SimultaneousServices = input.SimultaneousServices,
            Photo = input.Photo
        };
    }

    public EmployeeResponse ToDto(Employee input)
    {
        return new(
            input.Name,
            input.SimultaneousServices,
            input.Photo
        );
    }
}