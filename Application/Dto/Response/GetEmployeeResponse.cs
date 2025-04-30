namespace Application.Dto.Response;

public class GetEmployeeResponse
{
    public Guid Id { get; set; }
    public string Name {get; set;}
    public string CPF {get; set;}
    public int SimultaneousServices {get; set;}
    public string Email {get; set;}
    public string CorporateEmail {get; set;}
    public string Phone {get; set;}
}