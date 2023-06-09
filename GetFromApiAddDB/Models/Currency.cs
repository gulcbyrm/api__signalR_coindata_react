using System;
using System.Collections.Generic;

namespace GetFromApiAddDB.Models;

public partial class Currency
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public virtual ICollection<CurrencyAction> CurrencyActions { get; set; } = new List<CurrencyAction>();
}
