using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAll();
        Task<Supplier> GetSupplierById(int id);
        Task<Supplier> Add(Supplier supplier);
        Task<IDictionary<int, Supplier>> GetSuppliersByIds(IEnumerable<int> suppIds, CancellationToken cancellationToken);
    }
}
