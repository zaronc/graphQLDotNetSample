using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNet.API.GraphQL.InputTypes
{
    public class ReviewInput : InputObjectGraphType
    {
        public ReviewInput()
        {
            Name = "ReviewInput";
            Field<StringGraphType>("AuthorName");
            Field<IntGraphType>("Rate");
            Field<StringGraphType>("Comment");
            Field<IntGraphType>("ProductId");
        }
    }
}
