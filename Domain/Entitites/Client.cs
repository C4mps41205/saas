using Domain.Entitites.Base;
using Domain.Enum;

namespace Domain.Entitites;

public class Client : BaseEntity
{
    public required string Phone { get; set; }
    public required string Name { get; set; }
    public required string CpfCnpj { get; set; }
    public PersonType PersonType { get; set; }
    public required string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public State? State { get; set; }
    public string? City { get; set; }
    public string? Cep { get; set; }
    public int? Number { get; set; }
    public string? Neighborhood { get; set; }
    public string? Complement { get; set; }
    
    public Client? BelongsTo { get; set; }
    public Guid? BelongsToId { get; set; }
    public ICollection<Card> Cards { get; set; } = new List<Card>();
    public ICollection<Client> Subordinates { get; set; } = new List<Client>();
}