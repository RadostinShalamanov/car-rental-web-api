using System;

namespace Common.Entities;

public class CarLocation
{
    public int CarId { get; set; }
    public virtual Car Car { get; set; }

    public int LocationId { get; set; }
    public virtual Location Location { get; set; }
}
