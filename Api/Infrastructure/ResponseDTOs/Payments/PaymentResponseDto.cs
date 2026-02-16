using System;

namespace Api.Infrastructure.ResponseDTOs.Payments;

public class PaymentResponseDto
{
    public int Id { get; set; }

    public int RentalId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }
}
