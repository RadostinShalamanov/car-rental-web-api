using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Infrastructure.RequestDTOs.Features;

public class CarFeaturesUpdateDto
{   
    [Required]
    public List<int> FeatureIds { get; set; } = new List<int>();
}
