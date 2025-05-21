using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper.Base;
using Domain.Entitites;

namespace Application.Mapper;

public class CardMapper : IBaseMappper<CardResponse, Card, GetCardRequest>
{
    public Card ToEntity(GetCardRequest input)
    {
        throw new NotImplementedException();
    }

    public CardResponse ToDto(Card input)
    {   
        return new()
        {
            Id = input.Id,
            Title = input.Title,
            Description = input.Description,
            KanbanStatusCard = input.KanbanStatusCard,
            Clients = input.Clients.Select(x => x.Id).ToList() ?? [],
            Employee = input.Employee?.Id ?? Guid.Empty
        };
    }
}