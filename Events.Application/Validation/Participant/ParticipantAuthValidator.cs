using Events.Application.DTO.Participant;
using FluentValidation;

namespace Events.Application.Validation.Participant
{
    public class ParticipantAuthValidator : AbstractValidator<ParticipantAuthDTO>
    {
        public ParticipantAuthValidator() 
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("The email can't be empty")
                .MinimumLength(5).WithMessage("The email must contain at least 2 characters.")
                .EmailAddress().WithMessage("The email format is not valid.");

            RuleFor(e => e.Password).NotEmpty().WithMessage("The password can't be empty")
                .MinimumLength(8).WithMessage("The password must contain at least 8 characters.");
        }
    }
}
