using System;

namespace Api.Infrastructure.ResponseDTOs.Cars;

public class SearchAvailableCarsDto
{
    public int Id { get; set; }

    public string Brand { get; set; }

    public string Model { get; set; }

    public int Year { get; set; } 

    public decimal PricePerDay { get; set; }
}
