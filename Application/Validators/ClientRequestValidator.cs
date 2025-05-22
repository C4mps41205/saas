using Application.Dto.Request;
using FluentValidation;

namespace Application.Validators;

public class ClientRequestValidator : AbstractValidator<ClientRequest>
{
    public ClientRequestValidator()
    {
        RuleFor(client => client.Phone).NotEmpty().Length(13).Matches("^[0-9]*$");
        RuleFor(client => client.Name).NotEmpty().MaximumLength(256);
        RuleFor(client => client.CpfCnpj).Must(cpfCnpj => cpfCnpj.Length == 11 || cpfCnpj.Length == 14)
            .When(client => !string.IsNullOrEmpty(client.CpfCnpj));
        RuleFor(client => client.PersonType).NotNull();
        RuleFor(client => client.Email).NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(client => client.BirthDate).NotEmpty();
        RuleFor(client => client.State).NotNull();
        RuleFor(client => client.City).MaximumLength(255).When(client => !string.IsNullOrEmpty(client.City));
        RuleFor(client => client.Cep).Matches("^[0-9]*$").Length(8).When(client => !string.IsNullOrEmpty(client.Cep));
        RuleFor(client => client.Number).InclusiveBetween(1, 9999).When(client => client.Number.HasValue);
        RuleFor(client => client.Neighborhood).MaximumLength(255)
            .When(client => !string.IsNullOrEmpty(client.Neighborhood));
        RuleFor(client => client.Complement).MaximumLength(255)
            .When(client => !string.IsNullOrEmpty(client.Complement));
    }
}