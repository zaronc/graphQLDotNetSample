using GraphQL.Types;
using GraphQLDotNet.API.GraphQL.Types;

namespace GraphQLDotNet.API.GraphQL.InputTypes
{
    public class ProductInput : InputObjectGraphType
    {
        public ProductInput()
        {
            Name = "ProductInput";
            Field<StringGraphType>("Name");
            Field<StringGraphType>("Prezzo");
            Field<CategoryEnumType>("Category");
        }        
    }
}
