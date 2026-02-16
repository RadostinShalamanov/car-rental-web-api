using System;
using Api.Infrastructure.RequestDTOs.Auth;
using FluentValidation;

namespace Api.Infrastructure.Validation.RequestDTOsValidation.Auth;

public class AuthTokenRequestValidator:AbstractValidator<AuthTokenRequest>
{
    public AuthTokenRequestValidator()
    {
        RuleFor(t=>t.Username).NotEmpty().MinimumLength(4).WithMessage("Username must be at least 4 characters long.");
        
        RuleFor(t=>t.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

    }
}
