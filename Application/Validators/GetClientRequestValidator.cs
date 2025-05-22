using Application.Dto.Request;
using FluentValidation;

namespace Application.Validators;

public class GetClientRequestValidator : AbstractValidator<GetClientRequest>
{
    public GetClientRequestValidator()
    {
        RuleFor(x => x.Page)
            .NotEmpty()
            .WithMessage("Page is required");
        
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .WithMessage("PageNumber is required");        
        
        RuleFor(x => x.PageSize)
            .NotEmpty()
            .WithMessage("PageSize is required");
    }
}