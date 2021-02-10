using Domain.Models;
using Domain.Repository;
using Domain.Services;
using GraphQL;
using GraphQL.Types;
using GraphQLDotNet.API.GraphQL.InputTypes;
using GraphQLDotNet.API.GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GraphQLDotNet.API.GraphQL.Subscriptions.ShopSubscription;

namespace GraphQLDotNet.API.GraphQL.Mutations
{
    public class ShopMutations : ObjectGraphType
    {
        private readonly IReviewService _reviewService;

        public ShopMutations(ISupplierRepository supplierRepository, IProductRepository productRepository, IReviewService reviewService)
        {
            _reviewService = reviewService;

            FieldAsync<ProductType>(
                "createProduct",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductInput>>
                    {
                        Name = "productInput"
                    }
                ),
                resolve: async context =>
                {
                    var product = context.GetArgument<Product>("productInput");
                    product.SupplierId = 1;
                    // .. validazione ..
                    return await productRepository.Add(product);
                }
            );

            Field<ReviewType>(
                "createReview",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ReviewInput>>
                    {
                        Name = "reviewInput"
                    }
                ),
                resolve: context =>
                {
                    var review = context.GetArgument<Review>("reviewInput");
                    var rev = reviewService.Add(review);
                    return rev;
                }
            );
            
        }
    }
}
