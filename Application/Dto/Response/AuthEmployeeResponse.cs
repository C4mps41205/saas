namespace Application.Dto.Response;

public class AuthEmployeeResponse
{
    public string Token { get; set; }
    public EmployeeResponse Employee { get; set; }
}