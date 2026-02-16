using System;
using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


namespace Common.Services;

public class UserService
{
    private readonly CarRentalDbContext _dbContext;

    public UserService(CarRentalDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public List<User> GetAll()
    {
        return _dbContext.
                Users
                .Include(u=>u.Roles)
                .ThenInclude(ur=>ur.Role)
                .OrderBy(u=>u.Username)
                .ToList();
    }

    public User GetById(int id)
    {
       
        return _dbContext.Users
            .Include(u=>u.Roles)
            .ThenInclude(ur=>ur.Role)
            .FirstOrDefault(u => u.Id == id);
    }

    public List<Rental> GetRentals(int userId)
    {
        if (!_dbContext.Users.Any(u => u.Id == userId))
        {
            throw new InvalidOperationException("User not found/does not exist");
        }

        return _dbContext.Rentals
            .Include(r=>r.Payments)
            .Where(r=>r.UserId==userId)
            .OrderBy(r=>r.Id)
            .ToList();
    }

    public void Create(User item)
    {
        if (_dbContext.Users.Any(u => u.Username == item.Username))
        {
            throw new InvalidOperationException("Username already exists");
        }

        _dbContext.Users.Add(item);
        _dbContext.SaveChanges();
    }

    public void Update(User item)
    {
        _dbContext.Users.Update(item);
        _dbContext.SaveChanges();
    }

    public void Delete(User item)
    {
        if (item.Rentals.Count > 0)
        {
            throw new InvalidOperationException("User cannot be deleted because they have rentals");
        }

        _dbContext.Users.Remove(item);
        _dbContext.SaveChanges();
    }

    public int GetUsersCount()
    {
        var count = _dbContext.Users.Count();
        return count;
    }


    public void SetRoles(int userId, List<int> roleIds)
    {
        var user = _dbContext.Users.Include(u=>u.Roles)
        .FirstOrDefault(u=>u.Id==userId);


        if (user == null)
        {
            throw new Exception("User not found");
        }

        var validIds=_dbContext.Roles
        .Where(r=>roleIds.Contains(r.Id))
        .Select(r=>r.Id)
        .ToList();

        var userHavingIds=user.Roles.Select(r=>r.RoleId).ToList();
        var toRemove=userHavingIds.Except(roleIds).ToList();
        var toAdd=roleIds.Except(userHavingIds).ToList();

        user.Roles=user.Roles
            .Where(ur=>!toRemove.Contains(ur.RoleId))
            .ToList();

        foreach(var roleId in toAdd)
        {
            user.Roles.Add(new UserRoles
            {
                UserId=user.Id,
                RoleId=roleId
            });
        }

        _dbContext.SaveChanges();

    }
}
