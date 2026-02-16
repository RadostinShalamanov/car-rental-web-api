using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Infrastructure.RequestDTOs.Rental;

public class RentalCreateDto
{
    public int UserId { get; set; }
    public int CarId { get; set; }
    public int PickupLocationId { get; set; }
    public int ReturnLocationId { get; set; }
    public DateOnly PickupDate { get; set; }
    public DateOnly ReturnDate { get; set; }


}
