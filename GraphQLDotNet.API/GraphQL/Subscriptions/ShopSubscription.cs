using Domain.Models;
using Domain.Services;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using GraphQLDotNet.API.GraphQL.Types;
using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GraphQLDotNet.API.GraphQL.Subscriptions
{
    public partial class ShopSubscription : ObjectGraphType
    {
        private readonly IReviewService _reviewService;

        public ShopSubscription(IReviewService reviewService)
        {
            _reviewService = reviewService;
            AddField(new EventStreamFieldType
            {
                Name = "reviewAdded",
                Type = typeof(ReviewType),
                Resolver = new FuncFieldResolver<Review>(context => context.Source as Review),
                Subscriber = new EventStreamResolver<Review>(context => _reviewService.ReviewAdded())
            });
            
        }
    }
}
