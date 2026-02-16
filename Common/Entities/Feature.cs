using System;
using System.Collections.Generic;

namespace Common.Entities;

public class Feature:BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<CarFeature> CarFeatures { get; set; }
        = new List<CarFeature>();
}
