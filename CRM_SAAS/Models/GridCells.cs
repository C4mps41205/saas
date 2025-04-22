using Microsoft.AspNetCore.Components;

namespace CRM_SAAS.Models
{
    public class GridCells
    {
        public string Collumn { get; set; }
        public Func<dynamic, RenderFragment> Render { get; set; }
    }
}
