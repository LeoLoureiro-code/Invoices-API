using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices_API.DataAccess.EF.DTO
{
    public class CreateInvoiceDTO
    {
        [Required]
        public string ClientName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string ClientEmail { get; set; } = string.Empty;

        [Required]
        public DateOnly InvoiceDate { get; set; }

        [Required]
        public DateOnly InvoicePayment { get; set; }

        public string? ProjectDescription { get; set; }

        public List<ItemListDTO> Items { get; set; } = new();
    }
}
