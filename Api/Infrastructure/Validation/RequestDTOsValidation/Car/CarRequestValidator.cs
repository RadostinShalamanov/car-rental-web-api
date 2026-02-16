using System;
using System.Data;
using Api.Infrastructure.RequestDTOs.Car;
using FluentValidation;

namespace Api.Infrastructure.Validation.RequestDTOsValidation.Car;

public class CarRequestValidator:AbstractValidator<CarRequestDTO>
{
    public CarRequestValidator()
    {
        RuleFor(c=>c.Brand).NotEmpty().MaximumLength(20).WithMessage("String cannot be longer than 20 characters");
        RuleFor(c=>c.Model).NotEmpty().MaximumLength(20).WithMessage("String cannot be longer than 20 characters");
        RuleFor(c=>c.Year).InclusiveBetween(1900,2027).WithMessage("Invalid Year. Must be between 1900 and 2027");
        RuleFor(c=>c.PricePerDay).GreaterThan(0).WithMessage("Price cannot be less than or equal to 0");
    }
}
