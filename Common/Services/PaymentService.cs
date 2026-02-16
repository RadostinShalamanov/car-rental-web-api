using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Entities;

namespace Common.Services;

public class PaymentService
{
    private readonly CarRentalDbContext _context;

    public PaymentService(CarRentalDbContext context)
    {
        _context=context;
    }


    public IEnumerable<Payment> GetAll()
    {
        return _context.Payments
        .ToList();
    }

    public Payment GetById(int id)
    {
        return _context
            .Payments.FirstOrDefault(p=>p.Id==id);
    }


    public List<Payment> GetByRentalId(int rentalId)
    {
        return _context
            .Payments
            .Where(p=>p.RentalId==rentalId)
            .ToList();
    }


    public void Create(Payment item)
    {
        if (!_context.Rentals.Any(r => r.Id == item.RentalId))
        {
            throw new InvalidOperationException("Rental does not exist");
        }

        item.PaymentDate=DateTime.UtcNow;

        _context.Payments.Add(item);
        _context.SaveChanges();

        // _context.RentalStatusHistories.Add(new RentalStatusHistory
        // {
        //     RentalId=item.RentalId,
        //     Status="Payment added",
        //     ChangedAt=DateTime.UtcNow
        // });

        // _context.SaveChanges();
    }

    public void Delete(Payment item)
    {
        var toDelete=_context.Payments.FirstOrDefault(p=>p.Id==item.Id);
        if (toDelete == null)
        {
            throw new InvalidOperationException("Rental does not exist");
        }

        _context.Payments.Remove(item);
        _context.SaveChanges();
    }
}
