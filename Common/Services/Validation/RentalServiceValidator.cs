using System;
using System.Linq;
using Common.Entities;

namespace Common.Services.Validation;

public static class RentalServiceValidator
{
    public static void CreateValidation(
        Rental item,
        CarRentalDbContext _context)
    {
         if (item.EndDate < item.StartDate)
        {
            throw new InvalidOperationException("End date must be after start date");
        }

        if (!_context.Users.Any(u => u.Id == item.UserId))
        {
            throw new InvalidOperationException("User is invalid/does not exist");
        }

        if (!_context.Cars.Any(c => c.Id == item.CarId))
        {
            throw new InvalidOperationException("Car is invalid/does not exist");
        }

        if (!_context.Locations.Any(l => l.Id == item.PickupLocationId))
        {
            throw new InvalidOperationException("Pickup Location invalid/does not exist");
        }
        
        if (!_context.Locations.Any(l => l.Id == item.ReturnLocationId))
        {
            throw new InvalidOperationException("Return Location invalid/does not exist");
        }
    }

    public static void UpdateValidation(
        Rental currentRental,
        Rental updatedRental,
        CarRentalDbContext _context)
    {
         if (updatedRental.EndDate < updatedRental.StartDate)
        {
            throw new InvalidOperationException("End date must be after start date");
        }

        if (!_context.Locations.Any(l => l.Id == updatedRental.PickupLocationId))
        {
            throw new InvalidOperationException("Pickup Location invalid/does not exist");
        }
        
        if (!_context.Locations.Any(l => l.Id == updatedRental.ReturnLocationId))
        {
            throw new InvalidOperationException("Return Location invalid/does not exist");
        }
    }

    public static void CheckAvailability(
        Rental item,
        CarRentalDbContext _context,
        int? excludeRentalId = null)
    {
        bool overlaps=_context.Rentals.Any(r=>r.CarId==item.CarId&&
            (excludeRentalId==null|| r.Id!=excludeRentalId)&&
            !(r.EndDate<item.StartDate||r.StartDate>item.EndDate));

        if (overlaps)
        {
            throw new InvalidOperationException("Car is already rented for that period");
        }
    }
}
