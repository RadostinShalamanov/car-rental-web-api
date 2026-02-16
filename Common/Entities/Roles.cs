using System;
using System.Collections.Generic;

namespace Common.Entities;

public class Roles:BaseEntity
{
    public string RoleName { get; set; }
    public virtual ICollection<UserRoles> Users { get; set; } 
        = new List<UserRoles>();
}
