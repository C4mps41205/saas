namespace Application.Dto.Request;

public record EmployeeRequest(
    int PageNumber,
    int PageSize,
    int Page);