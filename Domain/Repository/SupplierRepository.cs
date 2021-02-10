using Domain.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ShopDbContext _dbContext;

        public SupplierRepository(ShopDbContext context)
        {
            this._dbContext = context;
        }

        public Task<List<Supplier>> GetAll()
        {
            return _dbContext.Suppliers.Include(i => i.Products).ToListAsync();
        }

        public Task<Supplier> GetSupplierById(int id)
        {
            return _dbContext.Suppliers.Include(i => i.Products).SingleOrDefaultAsync(sd => sd.Id == id);
        }

        public async Task<Supplier> Add(Supplier supplier)
        {
            var addedSupplier = await _dbContext.Suppliers.AddAsync(supplier);
            await _dbContext.SaveChangesAsync();
            return addedSupplier.Entity;
        }

        // per data loader
        public async Task<IDictionary<int, Supplier>> GetSuppliersByIds(IEnumerable<int> suppIds, CancellationToken cancellationToken)
        {
            return await _dbContext.Suppliers
                .Where(w => suppIds.Contains(w.Id))
                .ToDictionaryAsync(c => c.Id);
        }
    }
}
