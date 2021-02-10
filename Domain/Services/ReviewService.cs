using Domain.Models;
using Domain.Repository;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Domain.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private ISubject<Review> _sub = new ReplaySubject<Review>(1);

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Review Add(Review review)
        {
            var addedEntity = _reviewRepository.Add(review);
            _sub.OnNext(review);
            return review;
        }

        public IObservable<Review> ReviewAdded()
        {
            return _sub.AsObservable();
        }
    }
}
