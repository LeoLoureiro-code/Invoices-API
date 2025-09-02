using System;
using System.Collections.Generic;

namespace Invoices_API.DataAccess.EF.Models;

public partial class ItemList
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public uint Quantity { get; set; }

    public uint Price { get; set; }

    public uint InvoiceId { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
