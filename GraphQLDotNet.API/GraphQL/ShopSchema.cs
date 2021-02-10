using Domain.Repository;
using Domain.Services;
using GraphQL.Types;
using GraphQLDotNet.API.GraphQL.Mutations;
using GraphQLDotNet.API.GraphQL.Subscriptions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GraphQLDotNet.API.GraphQL
{
    public class ShopSchema : Schema
    {
        public ShopSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetService<ShopQuery>();
            Mutation = provider.GetService<ShopMutations>();
            Subscription = provider.GetService<ShopSubscription>();
        }
    }
}
