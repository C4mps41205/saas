using Application.Dto.Request;
using FluentValidation;

namespace Application.Validators;

public class GetClientByIdRequestValidator : AbstractValidator<GetClientByIdRequest>
{
    public GetClientByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
    }
}