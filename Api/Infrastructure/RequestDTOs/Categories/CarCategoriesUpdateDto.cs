using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Infrastructure.RequestDTOs.Categories;

public class CarCategoriesUpdateDto
{
    [Required]
    public List<int> CategoryIds { get; set; } = new List<int>();
}
