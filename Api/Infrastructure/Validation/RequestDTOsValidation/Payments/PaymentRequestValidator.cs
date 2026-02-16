using System;
using Api.Infrastructure.RequestDTOs.Payments;
using FluentValidation;

namespace Api.Infrastructure.Validation.RequestDTOsValidation.Payments;

public class PaymentRequestValidator:AbstractValidator<PaymentRequestDto>
{
    public PaymentRequestValidator()
    {
        RuleFor(p=>p.RentalId).NotEmpty();
        RuleFor(x=>x.Amount).NotEmpty().InclusiveBetween(50,5000).WithMessage("Amount must be between 50 and 5000");

    }
}
