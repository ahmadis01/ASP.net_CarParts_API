using CarParts.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Repositories.SharedRepository
{
    public interface ISharedRepository
    {
        Task<int> CalculateTotalAccount(ICollection<Invoice> invoices);
    }
}
