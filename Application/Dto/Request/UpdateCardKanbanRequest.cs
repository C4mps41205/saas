using Domain.Enum;

namespace Application.Dto.Request;

public record UpdateCardKanbanRequest(
    Guid Id,
    string Title,
    string Description,
    KanbanStatusCard KanbanStatusCard,
    List<Guid> Clients,
    Guid Employee
    );