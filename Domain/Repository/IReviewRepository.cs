using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IReviewRepository
    {
        Review Add(Review review);
        List<Review> GetReviewsByProductId(int productId);
    }
}
