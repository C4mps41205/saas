using System.Net;
using System.Net.Mail;
using Application.Repository;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    public bool Execute(string message, string subject, string toMail)
    {
        MailMessage mailMessage = new();

        mailMessage.From = new MailAddress(configuration["Smtp:Auth:Username"]!);
        mailMessage.Subject = subject;
        mailMessage.To.Add(new MailAddress(toMail));
        mailMessage.Body = message;
        mailMessage.IsBodyHtml = true;

        using SmtpClient smtp = new(configuration["Smtp:Host"]);
        smtp.Port = int.Parse(configuration["Smtp:Port"]!);
        smtp.Credentials = new NetworkCredential(configuration["Smtp:Auth:Username"], configuration["Smtp:Auth:Password"]);
        smtp.EnableSsl = true;

        smtp.Send(mailMessage);
        
        return true;
    }

    public async Task<string> ReplaceEmailParams(string path, string fileName, Dictionary<string, string> replacements)
    {
        var templatePath = Path.Combine(path, fileName);

        if (!File.Exists(templatePath))
            throw new FileNotFoundException("Template not found.", templatePath);

        var html = await File.ReadAllTextAsync(templatePath);

        foreach (var param in replacements)
        {
            html = html.Replace($"{{{{{param.Key}}}}}", param.Value);
        }
        
        return html;
    }
}