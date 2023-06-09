using System;
using System.Collections.Generic;

namespace GetFromApiAddDB.Models;

public partial class CurrencyAction
{
    public int Id { get; set; }

    public int CurrencyId { get; set; }

    public decimal Price { get; set; }

    public DateTime Date { get; set; }

    public virtual Currency Currency { get; set; } = null!;
}
