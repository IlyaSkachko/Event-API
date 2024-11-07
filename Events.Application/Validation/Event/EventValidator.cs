using Events.Application.DTO.Event;
using FluentValidation;

namespace Events.Application.Validation.Event
{
    public class EventValidator : AbstractValidator<EventDTO>
    {
        public EventValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("The event name can't be empty")
                .MinimumLength(2).WithMessage("The event name must contain at least 2 characters.");

            RuleFor(e => e.Description).NotEmpty().WithMessage("The event description can't be empty")
                .MinimumLength(2).WithMessage("The event description must contain at least 2 characters.");

            RuleFor(e => e.Location).NotEmpty().WithMessage("The event location can't be empty")
                .MinimumLength(2).WithMessage("The event location must contain at least 2 characters.");

            RuleFor(e => e.CategoryId).NotEmpty().WithMessage("The category can't be empty");

            RuleFor(e => e.MaxParticipants).NotEmpty().WithMessage("The max participants can't be empty")
                .GreaterThan(0).WithMessage("The max participants must be greater than 0.");
        }
    }
}
