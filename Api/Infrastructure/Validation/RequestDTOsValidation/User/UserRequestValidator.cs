using System;
using Api.Infrastructure.RequestDTOs.User;
using FluentValidation;

namespace Api.Infrastructure.Validation.RequestDTOsValidation.User;

public class UserRequestValidator:AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(u=>u.FirstName).NotEmpty().MinimumLength(3).MaximumLength(10).WithMessage("First Name must be between 3 and 10 characters long");
        RuleFor(u=>u.LastName).NotEmpty().MinimumLength(3).MaximumLength(20).WithMessage("Last Name must be between 3 and 20 characters long");
        RuleFor(u=>u.Username).NotEmpty().MinimumLength(5).MaximumLength(15).WithMessage("Username must be between 5 and 15 characters long");
        RuleFor(u=>u.Password).NotEmpty().MinimumLength(8).MaximumLength(20).WithMessage("Password must be between 8 and 20 characters long");

    }
}
