using Microsoft.AspNetCore.Components;

namespace CRM_SAAS.Models;

public class GridCells
{
    public string Collumn { get; set; }
    public Func<RenderFragment>? Render { get; set; }
}