namespace Application.Dto.Request;

public record GetClientRequest(int PageNumber, int PageSize, int TotalPages, int TotalCount, int Page);