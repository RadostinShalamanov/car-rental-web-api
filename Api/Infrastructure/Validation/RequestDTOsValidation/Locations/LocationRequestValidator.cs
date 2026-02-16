using System;
using Api.Infrastructure.RequestDTOs.Locations;
using FluentValidation;

namespace Api.Infrastructure.Validation.RequestDTOsValidation.Locations;

public class LocationRequestValidator:AbstractValidator<LocationRequestDto>
{
    public LocationRequestValidator()
    {
        RuleFor(l=>l.Address).NotEmpty().MinimumLength(5).MaximumLength(50).WithMessage("String must be between 5 and 50 characters long");
        RuleFor(l=>l.Name).NotEmpty().MaximumLength(30).WithMessage("Name cannot be longer than 30 characters");
        
    }
}
