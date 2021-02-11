using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IProductRepository
    {
        List<Product> GetProductsBySupplierId(int supplierId);
        Task<Product> Add(Product product);
        Task<List<Product>> GetAll();
    }
}
