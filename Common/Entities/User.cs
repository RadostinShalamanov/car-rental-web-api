using System;
using System.Collections.Generic;

namespace Common.Entities;

public class User:BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public virtual ICollection<UserRoles> Roles { get; set; } 
            = new List<UserRoles>();
    public virtual ICollection<Rental> Rentals { get; set; } 
            = new List<Rental>();
}
