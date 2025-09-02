using System;
using System.Collections.Generic;

namespace Invoices_API.DataAccess.EF.Models;

public partial class Invoice
{
    public uint Id { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? PostCode { get; set; }

    public string? Country { get; set; }

    public string? ClientName { get; set; }

    public string? ClientEmail { get; set; }

    public string? ClientStreet { get; set; }

    public string? ClientCity { get; set; }

    public string? ClientPostCode { get; set; }

    public string? ClientCountry { get; set; }

    public DateOnly? InvoiceDate { get; set; }

    public DateOnly? InvoicePayment { get; set; }

    public string? ProjectDescription { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<ItemList> ItemLists { get; set; } = new List<ItemList>();

    public virtual User User { get; set; } = null!;
}
