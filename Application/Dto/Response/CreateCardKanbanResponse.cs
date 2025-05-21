namespace Application.Dto.Response;

public class CreateCardKanbanResponse
{
    public string Title {get; set;}
    public string Description {get; set;}
    public string KanbanStatusCard {get; set;}
    public List<Guid> Clients {get; set;}
    public Guid Employee {get; set;}
}