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

        //Add a way to put item list the moment the invoice is created
        public async Task<Invoice> CreateInvoice(InvoiceDTO invoiceDTO, int userId)
        {

            var invoice = new Invoice
            {
                Street = invoiceDTO.Street,
                City = invoiceDTO.City,
                PostCode = invoiceDTO.PostCode,
                Country = invoiceDTO.Country,
                ClientName = invoiceDTO.ClientName,
                ClientEmail = invoiceDTO.ClientEmail,
                ClientStreet = invoiceDTO.ClientStreet,
                ClientCity = invoiceDTO.ClientCity,
                ClientPostCode = invoiceDTO.ClientPostCode,
                ClientCountry = invoiceDTO.ClientCountry,
                InvoiceDate = invoiceDTO.InvoiceDate,
                InvoicePayment = invoiceDTO.InvoicePayment,
                ProjectDescription = invoiceDTO.ProjectDescription,
                UserId = userId,
            };

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        //Add a way to put item list the moment the invoice is edited
        public async Task<Invoice> UpdateInvoice(InvoiceDTO invoiceDTO, int userId)
        {
            var existingInvoice = await _context.Invoices.FirstOrDefaultAsync(x => x.UserId == userId && x.UserId == userId);

            if(existingInvoice == null)
            {
                throw new Exception("Invoice not found");
            }

            existingInvoice.Street = invoiceDTO.Street;
            existingInvoice.City = invoiceDTO.City;
            existingInvoice.PostCode = invoiceDTO.PostCode;
            existingInvoice.Country = invoiceDTO.Country;
            existingInvoice.ClientName = invoiceDTO.ClientName;
            existingInvoice.ClientEmail = invoiceDTO.ClientEmail;
            existingInvoice.ClientStreet = invoiceDTO.ClientStreet;
            existingInvoice.ClientCity = invoiceDTO.ClientCity;
            existingInvoice.ClientPostCode = invoiceDTO.ClientPostCode;
            existingInvoice.ClientCountry = invoiceDTO.ClientCountry;
            existingInvoice.InvoiceDate = invoiceDTO.InvoiceDate;
            existingInvoice.InvoicePayment = invoiceDTO.InvoicePayment;
            existingInvoice.ProjectDescription = invoiceDTO.ProjectDescription;

            //fix this. Different types
            existingInvoice.ItemLists = invoiceDTO.Items;

            await _context.Invoices.AddAsync(existingInvoice);
            await _context.SaveChangesAsync();
            return existingInvoice;


        }

        public async Task DeleteInvoice(int id)
        {
            var existingInvoice = await _context.Invoices.FindAsync(id);

            if (existingInvoice == null)
            {
                throw new Exception("Invoice not found");
            }

            _context.Invoices.Remove(existingInvoice);
            await _context.SaveChangesAsync();
        }
    }
}
