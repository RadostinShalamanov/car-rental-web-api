using System;
using System.Collections.Generic;
using Api.Infrastructure.ResponseDTOs.Shared;

namespace Api.Infrastructure.ResponseDTOs.Rental;

public class RentalResponseDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CarId { get; set; }

    public int PickupLocationId { get; set; }

    public int ReturnLocationId { get; set; }

    public DateOnly PickupDate { get; set; }

    public DateOnly ReturnDate { get; set; }


    public List<IdNameDTO> Payments { get; set; } = new ();

    //public List<RentalStatusDto> StatusHistory {get;set;} = new();
}
