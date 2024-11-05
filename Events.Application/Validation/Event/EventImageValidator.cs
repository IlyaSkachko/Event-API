using Events.Application.DTO.Event;
using FluentValidation;

namespace Events.Application.Validation.Event
{
    public class EventImageValidator : AbstractValidator<EventImageDTO>
    {
        public EventImageValidator() 
        {
            RuleFor(e => e.Image).NotEmpty().WithMessage("Image cannot be null.")
                .Must(image => image.Length > 0).WithMessage("Image cannot be empty.")
                .Must(image => image.Length <= 5 * 1024 * 1024)
                .WithMessage("Image size must not exceed 5 MB.");
        }
    }
}
