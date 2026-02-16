using System;

namespace Api.Infrastructure.ResponseDTOs.Rental;

public class RentalStatusDto
{
    public string Status { get; set; }

    public DateTime ChangedAt { get; set; }
}
