using System;
using System.Collections.Generic;

namespace Common.Entities;

public class Category:BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<CarCategory> CarCategories { get; set; }
        = new List<CarCategory>();
}
