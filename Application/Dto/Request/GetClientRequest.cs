namespace Application.Dto.Request;

public record GetClientRequest(
    int PageNumber,
    int PageSize,
    int Page);
