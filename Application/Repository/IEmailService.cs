namespace Application.Repository;

public interface IEmailService
{
    bool Execute(string message, string subject, string toMail);
    Task<string> ReplaceEmailParams(string path, string fileName, Dictionary<string, string> replacements);
}