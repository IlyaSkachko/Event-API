using Events.Application.DTO.Event;
using FluentValidation;

namespace Events.Application.Validation.Event
{
    public class EventImageValidator : AbstractValidator<EventImageDTO>
    {
        public EventImageValidator() 
        {
            RuleFor(e => e.Image).NotEmpty().WithMessage("Image cannot be null.");
        }
    }
}
