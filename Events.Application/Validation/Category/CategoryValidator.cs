using Events.Application.DTO.Category;
using FluentValidation;

namespace Events.Application.Validation.Category
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator() 
        {
            RuleFor(category => category.Name)
                .NotEmpty()
                .WithMessage("The category name can't be empty")
                .MinimumLength(2)
                .WithMessage("The category name must contain at least 2 characters.")
                .Must(category => category.All(char.IsLetter))
                .WithMessage("All characters must be letters");
        }
    }
}
