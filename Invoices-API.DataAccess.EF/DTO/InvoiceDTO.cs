using Invoices_API.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices_API.DataAccess.EF.DTO
{
    public class InvoiceDto
    {
        public uint Id { get; set; }
        public string? ClientName { get; set; }
        public string? ClientEmail { get; set; }
        public DateOnly? InvoiceDate { get; set; }
        public DateOnly? InvoicePayment { get; set; }
        public string? ProjectDescription { get; set; }
        public List<ItemListDTO> Items { get; set; } = new();
    }
}
