using Domain.Models;
using GraphQL.Subscription;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public interface IReviewService
    {
        Review Add(Review episode);
        IObservable<Review> ReviewAdded();
    }
}
