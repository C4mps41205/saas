namespace Application.Dto.Request;

public record GetEmployeeRequest(
    int PageNumber,
    int PageSize,
    int Page);