using GraphQL.Types;
using GraphQLDotNet.API.GraphQL.Types;
using Domain.Repository;
using GraphQL;
using Domain.Models;
using System.Collections.Generic;

namespace GraphQLDotNet.API.GraphQL
{
    public class ShopQuery : ObjectGraphType
    {
        public ShopQuery(ISupplierRepository supplierRepository, IProductRepository productRepository, IReviewRepository reviewRepository)
        {
            #region product
            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context => productRepository.GetAll()
            );
            Field<ListGraphType<ProductType>>(
                "productsBySupplier",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context => productRepository.GetProductsBySupplierId(context.GetArgument<int>("id"))
            );
            
            #endregion

            #region supplier
            Field<ListGraphType<SupplierType>>(
               "suppliers",
               resolve: context => supplierRepository.GetAll()
            );

            Field<SupplierType>(
                "supplierById",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                    ),
                description: "Recupera un supplier specificando l'id.",
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return supplierRepository.GetSupplierById(id);
                }
            );
            #endregion

            #region review
            Field<ListGraphType<ReviewType>>(
                "reviewsByProduct",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context => reviewRepository.GetReviewsByProductId(context.GetArgument<int>("id"))
            );
            

            #endregion
        }
    }
}
