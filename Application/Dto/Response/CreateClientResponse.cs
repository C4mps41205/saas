using Domain.Enum;

namespace Application.Dto.Request;

public record CreateClientResponse(
    string Phone,
    string Name,
    string CpfCnpj,
    PersonType PersonType,
    string Email,
    DateTime BirthDate,
    State? State,
    string? City,
    string? Cep,
    int? Number,
    string? Neighborhood,
    string? Complement,
    List<CreateClientResponse> Subordinates 
        );