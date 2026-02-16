using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Common.Services.Validation;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Common.Services;

public class RentalService
{
    private readonly CarRentalDbContext _context;

    public RentalService(CarRentalDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Rental> GetAll()
    {
        return _context.Rentals
        .ToList();
    }


    public Rental GetById(int id)
    {
        return _context.Rentals
            .Include(r => r.Payments)
            .FirstOrDefault(r => r.Id == id);
    }

    public List<Rental> GetByCarId(int carId)
    {
        return _context.Rentals
            .Include(r=>r.Payments)
            .Where(r=>r.CarId==carId)
            .OrderBy(r=>r.Id)
            .ToList();
    }

    public List<Rental> GetByUserId(int userId)
    {
        return _context.Rentals
            .Include(r=>r.Payments)
            .Where(r=>r.UserId==userId)
            .OrderBy(r=>r.Id)
            .ToList();
    }

    public void Create(Rental item)
    {       
       RentalServiceValidator.CreateValidation(item,_context);
       RentalServiceValidator.CheckAvailability(item,_context);

        _context.Rentals.Add(item);
        _context.SaveChanges();

        // _context.RentalStatusHistories.Add(new RentalStatusHistory
        // {
        //     RentalId=item.Id,
        //     Status="Created",
        //     ChangedAt=DateTime.UtcNow
        // });

        // _context.SaveChanges();
    }

    public void Update(Rental item)
    {
        var toUpdate=_context.Rentals.FirstOrDefault(r=>r.Id==item.Id);
        if (toUpdate == null)
        {
            throw new ArgumentNullException("Rental not found");
        }

        item.CarId=toUpdate.CarId;

        RentalServiceValidator.UpdateValidation(toUpdate,item,_context);

        RentalServiceValidator.CheckAvailability(item,_context,excludeRentalId:toUpdate.Id);

        toUpdate.PickupLocationId=item.PickupLocationId;
        toUpdate.ReturnLocationId=item.ReturnLocationId;
        toUpdate.StartDate=item.StartDate;
        toUpdate.EndDate=item.EndDate;

        _context.Rentals.Update(item);
        _context.SaveChanges();
    }

    public void Delete(Rental item)
    {   
        var toDelete=_context.Rentals.Include(r=>r.Payments).FirstOrDefault(r=>r.Id==item.Id);

        if (toDelete == null)
        {
            throw new ArgumentNullException("Rental not found");
        }
       
        _context.Rentals.Remove(item);
        _context.SaveChanges();
    }


}
