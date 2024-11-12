using FluentValidation;
using Events.Application.DTO.Event;

namespace Events.Application.Validation.Event
{
    public class UpdateEventValidator : AbstractValidator<UpdateEventDTO>
    {
        public UpdateEventValidator()
        {
            RuleFor(_event => _event.Name).NotEmpty().WithMessage("The event name can't be empty")
                 .MinimumLength(2).WithMessage("The event name must contain at least 2 characters.");

            RuleFor(_event => _event.Description).NotEmpty().WithMessage("The event description can't be empty")
                .MinimumLength(2).WithMessage("The event description must contain at least 2 characters.");

            RuleFor(_event => _event.Location).NotEmpty().WithMessage("The event location can't be empty")
                .MinimumLength(2).WithMessage("The event location must contain at least 2 characters.");

            RuleFor(_event => _event.CategoryId).NotEmpty().WithMessage("The category can't be empty");

            RuleFor(_event => _event.MaxParticipants).NotEmpty().WithMessage("The max participants can't be empty")
                .GreaterThan(0).WithMessage("The max participants must be greater than 0.");
        }
    }
}
