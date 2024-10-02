using System;
using System.Collections.Generic;

namespace Practical.Models;

public partial class Car
{
    public int CarId { get; set; }

    public string Brand { get; set; } = null!;

    public int VersionId { get; set; }

    public int Year { get; set; }

    public virtual Version Version { get; set; } = null!;
}
