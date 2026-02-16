using System;

namespace Api.Infrastructure.ResponseDTOs.Locations;

public class LocationCarDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal PricePerDay { get; set; }
}
