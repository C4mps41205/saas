namespace Application.Dto.Response;

public record EmployeeResponse(
    string Name,
    int SimultaneousServices,
    string Photo
);