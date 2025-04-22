using Domain.Entitites.Base;

namespace Domain.Entitites;

public class Employee : BaseEntity
{
    public required string Name { get; set; }
    public required int SimultaneousServices { get; set; }
    public string Photo { get; set; }

    public List<Card> Cards { get; set; } = new List<Card>();
}