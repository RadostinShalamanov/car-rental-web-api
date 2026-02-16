using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Infrastructure.RequestDTOs.Payments;

public class PaymentRequestDto
{
    public int RentalId { get; set; }
    
    public decimal Amount { get; set; }
}
