using Application.Dto.Request;
using Application.Dto.Response;
using Application.Mapper.Base;
using Domain.Entitites;

namespace Application.Mapper;

public class CreateCardMapper : IBaseMappper<CreateCardKanbanResponse, Card, CreateCardKanbanRequest>
{
    public Card ToEntity(CreateCardKanbanRequest input)
    {
        return new()
        {
            Title = input.Title,
            Description = input.Description,
            KanbanStatusCard = input.KanbanStatusCard,
        };
    }

    public CreateCardKanbanResponse ToDto(Card input)
    {
        return new()
        {
            Title = input.Title,
            Description = input.Description,
            KanbanStatusCard = input.KanbanStatusCard.ToString(),
            Clients = input.Clients.Select(x => x.Id).ToList(),
            Employee = input.Employee.Id
        };
    }
}