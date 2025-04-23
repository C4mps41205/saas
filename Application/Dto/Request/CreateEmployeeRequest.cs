namespace Application.Dto.Request;

public record CreateEmployeeRequest(
    string Name,
    string CPF,
    int SimultaneousServices,
    string Email,
    string Password,
    string CorporateEmail,
    string Phone
);