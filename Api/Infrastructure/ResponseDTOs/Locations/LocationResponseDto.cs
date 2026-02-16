using System;
using System.Collections.Generic;

namespace Api.Infrastructure.ResponseDTOs.Locations;

public class LocationResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public List<LocationCarDto> Cars { get; set; }=new();
}
