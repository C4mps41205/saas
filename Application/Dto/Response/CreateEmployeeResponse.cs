namespace Application.Dto.Response;

public record CreateEmployeeResponse(
    string Name,
    int SimultaneousServices,
    string Photo
);