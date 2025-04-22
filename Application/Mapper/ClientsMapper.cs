using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper.Base;
using Domain.Entitites;

namespace Application.Mapper;

public class ClientsMapper : IBaseMappper<GetClientResponse, Client, GetClientRequest>
{
    public Client ToEntity(GetClientRequest input)
    {
        throw new NotImplementedException();
    }

    public GetClientResponse ToDto(Client input)
    {
        return new(
            input.Id,
            input.Phone,
            input.Name,
            input.CpfCnpj,
            input.PersonType,
            input.Email,
            input.BirthDate,
            input.State,
            input.City,
            input.Cep,
            input.Number,
            input.Neighborhood,
            input.Complement,
            input.Subordinates.Select(ToDto).ToList()
        );
    }
}