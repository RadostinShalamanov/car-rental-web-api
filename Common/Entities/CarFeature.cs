using System;

namespace Common.Entities;

public class CarFeature
{
    public int CarId { get; set; }
    public virtual Car Car { get; set; }

    public int FeatureId { get; set; }
    public virtual Feature Feature { get; set; }
}
