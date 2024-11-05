using Events.Application.DTO.Participant;
using FluentValidation;

namespace Events.Application.Validation.Participant
{
    public class UpdateParticipantValidator : AbstractValidator<UpdateParticipantDTO>
    {
        public UpdateParticipantValidator() 
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("Name can't be empty.")
                .MinimumLength(2)
                .WithMessage("Name must contain at least 2 characters.");

            RuleFor(u => u.Surname)
                .NotEmpty()
                .WithMessage("Surname can't be empty.")
                .MinimumLength(2)
                .WithMessage("Surname must contain at least 2 characters.");

            RuleFor(u => u.BirthDate)
                .NotEmpty()
                .WithMessage("Birth date can't be empty.")
                .LessThan(DateTime.Now)
                .WithMessage("Birth date must be in the past.");

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email can't be empty.")
                .EmailAddress()
                .WithMessage("Email format is not valid.")
                .MinimumLength(5)
                .WithMessage("Email must contain at least 5 characters.");
        }
    }
}
