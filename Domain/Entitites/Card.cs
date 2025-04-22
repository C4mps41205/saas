using Domain.Entitites.Base;
using Domain.Enum;

namespace Domain.Entitites;

public class Card : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public KanbanStatusCard KanbanStatusCard { get; set; }

    public ICollection<Client> Clients = new List<Client>();
    public Employee Employee { get; set; }
}