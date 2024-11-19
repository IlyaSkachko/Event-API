using Events.Application.DTO.Page;
using FluentValidation;

namespace Events.Application.Validation.Page
{
    public class PageValidator : AbstractValidator<PageDTO>
    {
        public PageValidator()
        {
            RuleFor(page => page.PageSize).NotEmpty()
                .GreaterThan(0)
                .WithMessage("Negative page size");

            RuleFor(page => page.PageNumber).NotEmpty()
                .GreaterThan(0)
                .WithMessage("Negative page number");
        }
    }
}
