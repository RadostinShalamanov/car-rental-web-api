using System;
using System.Collections.Generic;

namespace Common.Entities;

public class Car : BaseEntity
{
    public string Brand { get; set; }
    public string Model { get; set; }   
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }

    public virtual ICollection<Rental> Rentals { get; set; }
        = new List<Rental>();

    public virtual ICollection<CarCategory> CarCategories { get; set; }
        = new List<CarCategory>();
    public virtual ICollection<CarFeature> CarFeatures { get; set; }
        = new List<CarFeature>();
    public virtual ICollection<CarLocation> CarLocations { get; set; }
        = new List<CarLocation>();
}
