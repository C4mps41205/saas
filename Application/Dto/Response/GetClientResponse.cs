using Domain.Enum;

namespace Application.Dto.Response;

public record GetClientResponse
(
    Guid Id,
    string Phone,
    string Name,
    string CpfCnpj,
    PersonType PersonType,
    string Email,
    DateTime  BirthDate,
    State? State,
    string City,
    string Cep,
    int? Number,
    string Neighborhood,
    string Complement,
    List<GetClientResponse> Subordinates);