using Domain.Enum;

namespace Application.Dto.Request;

public record CreateCardKanbanRequest(
    string Title,
    string Description,
    KanbanStatusCard KanbanStatusCard,
    List<Guid> Clients,
    Guid Employee
);