using System;
using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Services;

public class LocationService
{
    private readonly CarRentalDbContext _context;

    public LocationService(CarRentalDbContext context)
    {
        _context = context;
    }


    public List<Location> GetAll()
    {
        return _context.Locations.ToList();
    }

    public Location GetById(int id)
    {
        return _context.Locations
        .Include(l=>l.CarLocations)
            .ThenInclude(l=>l.Car)
        .FirstOrDefault(l=>l.Id == id);
    }

    public void Create(Location item)
    {
        _context.Locations.Add(item);
        _context.SaveChanges();
    }

    public void Update(Location item)
    {
        _context.Locations.Update(item);
        _context.SaveChanges();
    }

    public void Delete(Location item)
    {
        bool used=_context.Rentals.Any(r=>r.PickupLocationId==item.Id||r.ReturnLocationId==item.Id);
        if(used)
        {
            throw new InvalidOperationException("Cannot delete a location that is associated with rentals.");
        }

        _context.Locations.Remove(item);
        _context.SaveChanges();
    }



}
