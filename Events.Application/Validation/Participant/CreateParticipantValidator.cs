using Events.Application.DTO.Participant;
using Events.Domain.Models;
using FluentValidation;

namespace Events.Application.Validation.Participant
{
    public class CreateParticipantValidator : AbstractValidator<CreateParticipantDTO>
    {
        public CreateParticipantValidator() 
        {
            RuleFor(participant => participant.Name)
                .NotEmpty()
                .WithMessage("Name can't be empty.")
                .MinimumLength(2)
                .WithMessage("Name must contain at least 2 characters.");

            RuleFor(participant => participant.Surname)
                .NotEmpty()
                .WithMessage("Surname can't be empty.")
                .MinimumLength(2)
                .WithMessage("Surname must contain at least 2 characters.");

            RuleFor(participant => participant.BirthDate)
                .NotEmpty()
                .WithMessage("Birth date can't be empty.")
                .LessThan(DateTime.Now)
                .WithMessage("Birth date must be in the past.");

            RuleFor(participant => participant.RegistrationDate)
                .NotEmpty()
                .WithMessage("Registration date can't be empty.")
                .GreaterThanOrEqualTo(participant => participant.BirthDate)
                .WithMessage("Registration date must be on or after birth date.")
                .Must((participant, registrationDate) =>
                    (registrationDate - participant.BirthDate).TotalDays >= 365 * 16)
                .WithMessage("User must be at least 16 years old.");

            RuleFor(participant => participant.Email)
                .NotEmpty()
                .WithMessage("Email can't be empty.")
                .EmailAddress()
                .WithMessage("Email format is not valid.")
                .MinimumLength(5)
                .WithMessage("Email must contain at least 5 characters.");

            RuleFor(participant => participant.Password)
                .NotEmpty()
                .WithMessage("The password can't be empty")
                .MinimumLength(8)
                .WithMessage("The password must contain at least 8 characters.");
        }
    }
}
