using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper.Base;
using Domain.Entitites;

namespace Application.Mapper;

public class EmployeeMapper : IBaseMappper<EmployeeResponse, Employee, EmployeeRequest>
{
    public Employee ToEntity(EmployeeRequest input)
    {
        throw new NotImplementedException();
    }

    public EmployeeResponse ToDto(Employee input)
    {
        return new(
            input.Name,
            input.CPF,
            input.SimultaneousServices,
            input.Email,
            input.CorporateEmail,
            input.Phone
        );
    }
}