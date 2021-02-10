using Domain.Models;
using Domain.Repository;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace GraphQLDotNet.API.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(ISupplierRepository supplierRepository, IDataLoaderContextAccessor accessor)
        {
            Name = "Product";
            Field(_ => _.Id, type: typeof(IntGraphType)).Description("L'Id del prodotto.");
            Field(_ => _.Name).Description("Nome del prodotto.");
            Field(_ => _.Prezzo, type: typeof(DecimalGraphType)).Description("Prezzo del prodotto.");
            Field(_ => _.Category, type: typeof(CategoryEnumType)).Description("Prezzo del prodotto.");
            
            Field<SupplierType>(
                "Supplier",
                //resolve: context => supplierRepository.GetSupplierById(context.Source.SupplierId).Result
                //resolve: context => context.Source.Supplier
                resolve: context =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<int, Supplier>("GetSuppliersByIds", supplierRepository.GetSuppliersByIds);
                    return loader.LoadAsync(context.Source.SupplierId);
                }
            );
        }        
    }
}
