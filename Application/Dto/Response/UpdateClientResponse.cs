using Domain.Enum;

namespace Application.Dto.Response;

public class UpdateClientResponse()
{
    public string Phone { get; set; }
    public string Name { get; set; }
    public string CpfCnpj { get; set; }
    public PersonType PersonType { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public State? State { get; set; }
    public string? City { get; set; }
    public string? Cep { get; set; }
    public int? Number { get; set; }
    public string? Neighborhood { get; set; }
    public string? Complement { get; set; }
    public List<Guid> Subordinates { get; set; }
};