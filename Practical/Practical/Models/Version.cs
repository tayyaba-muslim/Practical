using System;
using System.Collections.Generic;

namespace Practical.Models;

public partial class Version
{
    public int VersionId { get; set; }

    public int ModelId { get; set; }

    public string VersionName { get; set; } = null!;

    public string EngineType { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual Model Model { get; set; } = null!;
}
