using System;

namespace Common.Entities;

public class RentalStatusHistory:BaseEntity
{   
    public int RentalId { get; set; }

    public virtual Rental Rental { get; set; }

    public string Status { get; set; }

    public DateTime ChangedAt { get; set; }
}
