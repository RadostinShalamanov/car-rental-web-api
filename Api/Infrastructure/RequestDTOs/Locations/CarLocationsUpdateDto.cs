using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Infrastructure.RequestDTOs.Locations;

public class CarLocationsUpdateDto
{
    [Required]
    public List<int> LocationIds { get; set; } = new List<int>();
}
