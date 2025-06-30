using FluentValidation;
using StudyMate.Application.DTOs;

namespace StudyMate.Application.Validator;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .MaximumLength(100).WithMessage("Password must be between 6 and 100 characters")
            .Matches(@"[\!\@\#\$\%\^\&\*\-]")
            .WithMessage("Password must contain at least one special character: !\\@\\#\\$\\%\\^\\&\\*\\-")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter: A-Z");
    }
}