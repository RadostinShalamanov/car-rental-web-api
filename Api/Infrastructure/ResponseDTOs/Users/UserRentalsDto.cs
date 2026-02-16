using System;

namespace Api.Infrastructure.ResponseDTOs.Users;

public class UserRentalsDto
{
    public int Id { get; set; }

    public int CarId { get; set; }

    public int PickupLocationId { get; set; }
    public int ReturnLocationId { get; set; }

    public DateOnly PickupDate { get; set; }
    public DateOnly ReturnDate { get; set; }

    public decimal TotalPaid { get; set; }
}
