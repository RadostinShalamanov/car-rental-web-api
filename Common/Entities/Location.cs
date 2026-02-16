using System;
using System.Collections.Generic;

namespace Common.Entities;

public class Location:BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }

    public virtual ICollection<CarLocation> CarLocations { get; set; }
        = new List<CarLocation>();
    public virtual ICollection<Rental> PickupRentals { get; set; }
        = new List<Rental>();
    public virtual ICollection<Rental> ReturnRentals { get; set; }
        = new List<Rental>();
}
