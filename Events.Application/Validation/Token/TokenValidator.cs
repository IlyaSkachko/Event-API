using Events.Application.DTO.Token;
using FluentValidation;

namespace Events.Application.Validation.Token
{
    using FluentValidation;

    public class TokenValidator : AbstractValidator<TokenDTO>
    {
        public TokenValidator()
        {
            RuleFor(token => token.Access)
                .NotEmpty().WithMessage("Access token is required.");

            RuleFor(token => token.AccessExpires).NotEmpty().WithMessage("Access expires is required.");

            RuleFor(token => token.Refresh)
                .NotEmpty().WithMessage("Refresh token is required.");

            RuleFor(token => token.RefreshExpires).NotEmpty()
                .WithMessage("Refresh expires is required.");
        }
    }

}
