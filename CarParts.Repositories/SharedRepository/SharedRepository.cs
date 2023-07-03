using CarParts.Models.Main;
using CarParts.SharedKernal.Enums;
using CarParts.SqlServer.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Repositories.SharedRepository
{
    public class SharedRepository : ISharedRepository
    {
        private readonly CarPartContext _context;
        public SharedRepository(CarPartContext context)
        {
            _context = context;
        }
        public async Task<int> CalculateTotalAccount(ICollection<Invoice> invoices)
        {
            var totalAccount = 0;
            foreach (var invoice in invoices)
            {
                if (invoice.InvoiceType == InvoiceType.PurchaseInvoice || invoice.InvoiceType == InvoiceType.OutgoingPayment)
                {
                    totalAccount -= invoice.Cost + invoice.Services;
                }
                else if (invoice.InvoiceType == InvoiceType.SellInvoice || invoice.InvoiceType == InvoiceType.IncomingPayment)
                    totalAccount += invoice.Cost + invoice.Services;
            }
            return totalAccount;
        }
    }
}
