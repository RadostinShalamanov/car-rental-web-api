using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Infrastructure.RequestDTOs.Rental;

public class RentalUpdateDto
{
    public int PickupLocationId { get; set; }
    public int ReturnLocationId { get; set; }

    public DateOnly PickupDate { get; set; }
    public DateOnly ReturnDate { get; set; }
}
