using Domain.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _dbContext;

        public ProductRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Add(Product product)
        {
            var addedProduct = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return addedProduct.Entity;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _dbContext.Products.Include(i=>i.Supplier).ToListAsync();
        }

        public List<Product> GetProductsBySupplierId(int supplierId)
        {
            return _dbContext.Products.Where(w => w.SupplierId == supplierId).ToList();
        }
    }
}
