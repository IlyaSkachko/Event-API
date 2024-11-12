using Events.Application.DTO.Participant;
using FluentValidation;

namespace Events.Application.Validation.Participant
{
    public class ParticipantValidator : AbstractValidator<ParticipantDTO>
    {
        public ParticipantValidator() 
        {
            RuleFor(participant => participant.Name)
                .NotEmpty().WithMessage("Name can't be empty.")
                .MinimumLength(2).WithMessage("Name must contain at least 2 characters.");

            RuleFor(participant => participant.Surname)
                .NotEmpty().WithMessage("Surname can't be empty.")
                .MinimumLength(2).WithMessage("Surname must contain at least 2 characters.");

            RuleFor(participant => participant.BirthDate)
                .NotEmpty().WithMessage("Birth date can't be empty.")
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");

            RuleFor(participant => participant.RegistrationDate)
                .NotEmpty().WithMessage("Registration date can't be empty.")
                .GreaterThanOrEqualTo(u => u.BirthDate).WithMessage("Registration date must be on or after birth date.");

            RuleFor(participant => participant.Email)
                .NotEmpty().WithMessage("Email can't be empty.")
                .EmailAddress().WithMessage("Email format is not valid.")
                .MinimumLength(5).WithMessage("Email must contain at least 5 characters.");
        }
    }
}
