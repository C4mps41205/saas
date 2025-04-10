using Domain.Entitites.Base;
using Domain.Enum;

namespace Domain.Entitites;

public class Client : BaseEntity
{
    public required string Phone { get; set; }
    public required string Name { get; set; }
    public required string CpfCnpj { get; set; }
    public PersonType PersonType { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public State? State { get; set; }
    public string? City { get; set; }
    public string? Cep { get; set; }
    public int? Number { get; set; }
    public string? Neighborhood { get; set; }
    public string? Complement { get; set; }
    
    public Client BelongsTo { get; set; }
    public Guid BelongsToId { get; set; }
}