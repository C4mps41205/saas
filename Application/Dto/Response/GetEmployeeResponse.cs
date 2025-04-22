using System.Reflection.Metadata.Ecma335;

namespace Application.Dto.Response;

public class GetEmployeeResponse
{
    public string Name { get; set; }
    public int SimultaneousServices { get; set; }
}