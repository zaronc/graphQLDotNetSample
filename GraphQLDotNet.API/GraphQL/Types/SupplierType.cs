using GraphQL.Types;
using Domain.Models;
using Domain.Repository;

namespace GraphQLDotNet.API.GraphQL.Types
{
    public class SupplierType : ObjectGraphType<Supplier>
    {
        public SupplierType(IProductRepository productRepository)
        {
            Name = "Supplier";
            Field(_ => _.Id, type: typeof(IntGraphType)).Description("L'Id del fornitore.");
            Field(_ => _.Name).Description("Nome del fornitore.");
            Field(_ => _.Email).Description("Email del fornitore.");

            // recupera tutti i prodotti associati al supplier
            Field<ListGraphType<ProductType>>(
                "Products",
                resolve: context => productRepository.GetProductsBySupplierId(context.Source.Id)
                //resolve: context => context.Source.Products
            );
        }
    }
}
