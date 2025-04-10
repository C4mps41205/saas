using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper.Base;
using Domain.Entitites;

namespace Application.Mapper;

public class CreateClientMapper: IBaseMappper<CreateClientResponse, Client, CreateClientRequest>
{
    public Client ToEntity(CreateClientRequest input)
    {
        throw new NotImplementedException();
    }

    public CreateClientResponse ToDto(Client input)
    {
        throw new NotImplementedException();
    }
}