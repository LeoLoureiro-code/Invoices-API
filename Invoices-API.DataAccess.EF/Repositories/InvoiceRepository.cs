using Invoices_API.DataAccess.EF.Context;
using Invoices_API.DataAccess.EF.DTO;
using Invoices_API.DataAccess.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices_API.DataAccess.EF.Repositories
{
    public class InvoiceRepository
    {
        private readonly InvoicesDbContext _context;

        public InvoiceRepository( InvoicesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesByUserId(int id)
        {
            return await _context.Invoices
                .Where(x => x.UserId == id)
                .ToListAsync();
        }

        public async Task<Invoice>GetInvoiceById(int invoiceId, int userId)
        {
            return await _context.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId && x.UserId == userId);
        }

        //public async Task<Invoice> CreateInvoice(CreateInvoiceDTO dto, int userId)
        //{
          
        //    var invoice = new Invoice
        //    {
        //        Street = dto.Street,
        //        City = dto.City,
        //        PostCode = dto.PostCode,
        //        Country = dto.Country,
        //        ClientName = dto.ClientName,
        //        ClientEmail = dto.ClientEmail,
        //        ClientStreet = dto.ClientStreet,
        //        ClientCity = dto.ClientCity,
        //        ClientPostCode = dto.ClientPostCode,
        //        ClientCountry = dto.ClientCountry,
        //        InvoiceDate = dto.InvoiceDate,
        //        InvoicePayment = dto.InvoicePayment,
        //        ProjectDescription = dto.ProjectDescription,
        //        UserId = userId,
        //        ItemLists = dto.Items.Select(i => new ItemList
        //        {
        //            Name = i.Name,
        //            Quantity = i.Quantity,
        //            Price = i.Price
        //        }).ToList()
        //    };

        //    await _context.Invoices.AddAsync(invoice);
        //    await _context.SaveChangesAsync();

        //    return invoice;
        //}
    }
}
