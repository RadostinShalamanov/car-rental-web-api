using System;
using Api.Infrastructure.RequestDTOs.Rental;
using FluentValidation;

namespace Api.Infrastructure.Validation.RequestDTOsValidation.Rental;

public class RentalUpdateValidator:AbstractValidator<RentalUpdateDto>
{
    public RentalUpdateValidator()
    {
        RuleFor(r=>r.PickupLocationId).GreaterThan(0).WithMessage("PickupLocationId be less or equal to 0");
        RuleFor(r=>r.ReturnLocationId).GreaterThan(0).WithMessage("ReturnLocationId be less or equal to 0");
        RuleFor(r=>r.ReturnDate).GreaterThanOrEqualTo(r=>r.PickupDate).WithMessage("Return date cannot be before pickup date");
    }
}
