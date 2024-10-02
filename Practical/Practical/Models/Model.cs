using System;
using System.Collections.Generic;

namespace Practical.Models;

public partial class Model
{
    public int ModelId { get; set; }

    public string ModelName { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ICollection<Version> Versions { get; set; } = new List<Version>();
}
