using Domain.Enum;

namespace Application.Dto.Request;

public record ChangeCardStatusRequest(Guid Id, KanbanStatusCard KanbanStatusCard);