using Domain.Enum;

namespace Application.Dto.Response;

public class CardResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public KanbanStatusCard KanbanStatusCard { get; set; }
    public List<Guid> Clients { get; set; }
    public Guid Employee { get; set; }
}