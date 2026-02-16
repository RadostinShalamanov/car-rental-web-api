using System;

namespace Common.Entities;

public class CarCategory
{
    public int CarId { get; set; }
    public virtual Car Car { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}
