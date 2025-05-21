using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper.Base;
using Domain.Entitites;

namespace Application.Mapper;

public class CreateClientMapper : IBaseMappper<CreateClientResponse, Client, ClientRequest>
{
    public Client ToEntity(ClientRequest input)
    {
        var client = new Client
        {
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
            Subordinates = new List<Client>()
        };

        // if (input.Subordinates is not null && input.Subordinates.Any())
        // {
        //     foreach (var subordinate in input.Subordinates)
        //     {
        //         var entity = ToEntity(subordinate);
        //         entity.BelongsTo = client;
        //         client.Subordinates.Add(entity);
        //     }
        // }

        return client;
    }

    public CreateClientResponse ToDto(Client input)
    {
        return new()
        {
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