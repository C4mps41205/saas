using Domain.Entitites.Base;

namespace Domain.Entitites;

public class Employee : BaseEntity
{
    public required string Name { get; set; }
    public required string CPF { get; set; }
    public required int SimultaneousServices { get; set; }
    public required string Email { get; set; }
    public required string CorporateEmail { get; set; }
    public string PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public required string Phone { get; set; }
    public string? Photo { get; set; }
    public List<Card> Cards { get; set; } = new List<Card>();
}