using System;
using System.Collections.Generic;

namespace Common.Entities;

public class Rental:BaseEntity
{
    public int UserId { get; set; }
    public int CarId { get; set; }

    public int PickupLocationId { get; set; }
    public int ReturnLocationId { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public virtual User User { get; set; }
    public virtual Car Car { get; set; }

    public virtual Location PickupLocation { get; set; }
    public virtual Location ReturnLocation { get; set; }

    public virtual ICollection<Payment> Payments { get; set; }
        = new List<Payment>();

    public virtual List<RentalStatusHistory> StatusHistory { get; set; }=new();
}
