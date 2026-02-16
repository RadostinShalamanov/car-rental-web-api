using System;

namespace Common.Entities;

public class Payment:BaseEntity
{
    public int RentalId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public virtual Rental Rental { get; set; }
}
