using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Services;

public class CarService
{
    private readonly CarRentalDbContext _context;

    public CarService(CarRentalDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Car> GetAll()
    {
        return _context.Cars
        .ToList();
    }


    public Car GetById(int id)
    {
        return _context.Cars.Include(c=>c.CarCategories)
        .ThenInclude(cc=>cc.Category)
        .Include(c=>c.CarFeatures)
        .ThenInclude(cf=>cf.Feature)
        .Include(c=>c.CarLocations)
        .ThenInclude(cl=>cl.Location)
        .FirstOrDefault(c => c.Id == id);
    }

    public void Create(Car item)
    {
        _context.Cars.Add(item);
        _context.SaveChanges();
    }

    public void Update(Car item)
    {
        _context.Cars.Update(item);
        _context.SaveChanges();
    }

    public void Delete(Car item)
    {   
    
        if (item.Rentals.Count > 0)
        {
            throw new InvalidOperationException("Cannot delete a car that has associated rentals.");
        }
        _context.Cars.Remove(item);
        _context.SaveChanges();
    }


    public List<Car> SearchAvailableCars(DateOnly pickupDate,DateOnly returnDate)
    {
        if (returnDate < pickupDate)
        {
            throw new InvalidOperationException("Return date cannot be before the pickup date");
        }

       

        var cars=_context.Cars
            .Where(c=>!_context.Rentals.Any
                (r=>r.CarId==c.Id &&
                !(r.EndDate<pickupDate||r.StartDate>returnDate)
                ));
        return cars.OrderBy(c=>c.Brand).ThenBy(c=>c.Model).ToList();
    }


    public void SetCategories(int carId,List<int> categoryIds)
    {
        
        var car =_context.Cars
            .Include(c=>c.CarCategories)
            .FirstOrDefault(c=>c.Id==carId);
        if (car == null)
        {
            throw new Exception("Car not found");               
        }

        var validIds= _context.Categories
            .Where(c=>categoryIds.Contains(c.Id))
            .Select(c=>c.Id)
            .ToList();

        var carHavingIds=car.CarCategories.Select(cc=>cc.CategoryId).ToList();
        var toRemove=carHavingIds.Except(categoryIds).ToList();
        var toAdd=categoryIds.Except(carHavingIds).ToList();

        car.CarCategories=car.CarCategories
            .Where(cc=>!toRemove.Contains(cc.CategoryId))
            .ToList();

        foreach(var categoryId in toAdd)
        {
            car.CarCategories.Add(new CarCategory
            {
                CarId=car.Id,
                CategoryId=categoryId
            });
        }

        _context.SaveChanges();

    }

    public void SetFeatures(int carId,List<int> featureIds)
    {
        var car =_context.Cars
            .Include(c=>c.CarFeatures)
            .FirstOrDefault(c=>c.Id==carId);

        if (car == null)
        {
            throw new Exception("Car not found");               
        }

        var validIds= _context.Features
            .Where(f=>featureIds.Contains(f.Id))
            .Select(f=>f.Id)
            .ToList();  

        var carHavingIds=car.CarFeatures.Select(f=>f.FeatureId).ToList();
        var toRemove=carHavingIds.Except(featureIds).ToList();
        var toAdd=featureIds.Except(carHavingIds).ToList();

        car.CarFeatures=car.CarFeatures
            .Where(cf=>!toRemove.Contains(cf.FeatureId))
            .ToList();

        foreach (var featureId in toAdd)
        {
            car.CarFeatures.Add(new CarFeature
            {
                CarId=car.Id,
                FeatureId=featureId
            });
        }

        _context.SaveChanges();
    }


    public void SetLocations(int carId,List<int> locationIds)
    {
        var car =_context.Cars
            .Include(c=>c.CarLocations)
            .FirstOrDefault(c=>c.Id==carId);

        if (car == null)
        {
            throw new Exception("Car not found");               
        }

        var validIds= _context.Locations
            .Where(l=>locationIds.Contains(l.Id))
            .Select(l=>l.Id)
            .ToList();  

        var carHavingIds=car.CarLocations.Select(l=>l.LocationId).ToList();
        var toRemove=carHavingIds.Except(locationIds).ToList();
        var toAdd=locationIds.Except(carHavingIds).ToList();

        car.CarLocations=car.CarLocations
            .Where(cl=>!toRemove.Contains(cl.LocationId))
            .ToList();

        foreach (var locationId in toAdd)
        {
            car.CarLocations.Add(new CarLocation
            {
                CarId=car.Id,
                LocationId=locationId
            });
        }

        _context.SaveChanges();
    }
}
    