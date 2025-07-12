using FluentValidation;
using StudyMate.Application.DTOs;
using StudyMate.Application.DTOs.Course;

namespace StudyMate.Application.Validator;

public class CourseCreateValidator : AbstractValidator<CourseCreateDto>
{
    public CourseCreateValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(50).WithMessage("Title must not exceed 50 characters");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(50).WithMessage("Description must not exceed 50 characters");
        
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        
        RuleFor(x => x.ReleaseYear)
            .NotEmpty().WithMessage("ReleaseYear is required")
            .GreaterThan(1500).WithMessage("ReleaseYear must be greater than 1500")
            .LessThan(2026).WithMessage("ReleaseYear must be greater than 2026");
    }
}