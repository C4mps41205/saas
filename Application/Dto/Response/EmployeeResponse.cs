namespace Application.Dto.Response;

public record EmployeeResponse(
    Guid Id,
    string Name,
    string CPF,
    int SimultaneousServices,
    string Email,
    string CorporateEmail,
    string Phone
);