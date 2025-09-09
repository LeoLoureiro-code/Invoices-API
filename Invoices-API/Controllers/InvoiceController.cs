using Invoices_API.DataAccess.EF.Context;
using Invoices_API.DataAccess.EF.DTO;
using Invoices_API.DataAccess.EF.Models;
using Invoices_API.DataAccess.EF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoices_API.Controllers
{
    [Route("api/Invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {

        private readonly InvoiceRepository _invoiceRepository;

        public InvoiceController(InvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [Authorize]
        [HttpGet("get-invoices-by-user")]
        public async Task <ActionResult <IEnumerable<Invoice>>> GetInvoicesByUserId(int id)
        {
            try
            {
                var invoices =  await _invoiceRepository.GetAllInvoicesByUserId(id);

                if (invoices == null || !invoices.Any())
                {
                    return NotFound("No invoices found for this user");
                }

                return Ok(invoices);
            }
            catch(Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while fetching invoices for this user.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            
        }

        [Authorize]
        [HttpPost("create-invoice")]
        public async Task <ActionResult<Invoice>> CreateInvoice([FromBody] InvoiceDTO invoice, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        Message = "Email and password are required."
                    });
                }

                var createdInvoice = await _invoiceRepository.CreateInvoice(invoice, id);

                return createdInvoice;
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.InnerException?.Message ?? ex.Message,
                    title: "An error occurred while creating the invoice.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
