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
        throw new NotImplementedException();
    }
}