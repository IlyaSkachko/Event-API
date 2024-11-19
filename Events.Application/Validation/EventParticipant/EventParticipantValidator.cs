using Events.Application.DTO.EventParticipant;
using FluentValidation;

namespace Events.Application.Validation.EventParticipant
{
    public class EventParticipantValidator : AbstractValidator<EventParticipantDTO>
    {
        public EventParticipantValidator() 
        {
            RuleFor(eventParticipant => eventParticipant.ParticipantId)
                .NotEmpty()
                .WithMessage("Participant is required.");

            RuleFor(eventParticipant => eventParticipant.EventId)
                 .NotEmpty()
                .WithMessage("Event is required.");
        }
    }
}
