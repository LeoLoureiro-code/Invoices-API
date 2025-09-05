using Invoices_API.DataAccess.EF.DTO;
using Invoices_API.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices_API.DataAccess.EF.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<IEnumerable<Invoice>> GetAllInvoicesByUserId(int id);

        public Task<Invoice> GetInvoiceById(int invoiceId, int userId);

        public Task<Invoice> CreateInvoice(InvoiceDTO invoiceDTO, int userId);

        public Task<Invoice> UpdateInvoice(InvoiceDTO invoiceDTO, int userId);

        public Task DeleteInvoice(int id);
    }
}
