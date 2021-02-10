using Domain.Context;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private ShopDbContext _dbContext;
        private readonly IServiceProvider _provider;

        public ReviewRepository(IServiceProvider provider)
        {
            
            _provider = provider;
        }

        public Review Add(Review review)
        {
            using (var scope = _provider.CreateScope())
            {
                _dbContext = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
                var addedSupplier = _dbContext.Reviews.Add(review);
                _dbContext.SaveChanges();
                return addedSupplier.Entity;
            }
        }

        public List<Review> GetReviewsByProductId(int productId)
        {
            using (var scope = _provider.CreateScope())
            {
                _dbContext = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
                var reviews = _dbContext.Reviews.Where(w=>w.ProductId == productId).ToList();
                return reviews;
            }
        }
    }
}
