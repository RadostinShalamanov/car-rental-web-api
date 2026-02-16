using System;
using System.Collections;
using System.Collections.Generic;
using Api.Infrastructure.RequestDTOs.Categories;

namespace Api.Infrastructure.RequestDTOs.Car;

public class CarRequestDTO
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }

}
