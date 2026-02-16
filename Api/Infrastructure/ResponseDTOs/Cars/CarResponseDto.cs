using System;
using System.Collections.Generic;
using Api.Infrastructure.RequestDTOs.Categories;
using Api.Infrastructure.ResponseDTOs.Shared;

namespace Api.Infrastructure.ResponseDTOs.Cars;

public class CarResponseDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public List<IdNameDTO> Categories { get; set; }

    public List<IdNameDTO> Features { get; set;  }

    public List<IdNameDTO> Locations { get; set;  }
}
