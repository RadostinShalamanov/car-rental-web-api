using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Infrastructure.RequestDTOs.Locations;

public class LocationRequestDto
{
    public string Name { get; set; }
    public string Address { get; set; }
}
