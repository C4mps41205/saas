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
        return new()
        {
            Id = input.Id,
            Phone = input.Phone,
            Name = input.Name,
            CpfCnpj = input.CpfCnpj,
            PersonType = input.PersonType,
            Email = input.Email,
            BirthDate = input.BirthDate,
            State = input.State,
            City = input.City,
            Cep = input.Cep,
            Number = input.Number,
            Neighborhood = input.Neighborhood,
            Complement = input.Complement,
            Subordinates = input.Subordinates.Select(ToDto).ToList()
        };
    }
}