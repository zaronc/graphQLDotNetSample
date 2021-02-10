using Domain.Models;
//using Domain.Repository;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNet.API.GraphQL.Types
{
    public class ReviewType : ObjectGraphType<Review>
    {
        public ReviewType()
        {
            Name = "Review";

            Field(_ => _.AuthorName).Description("Nome del recensore.");
            Field(_ => _.Rate).Description("Voto.");
            Field(_ => _.Comment).Description("Commento recensione.");
            Field(_ => _.ProductId);
            //Field<ProductType>(
            //    "Product",
            //    resolve: context => context.Source.Product
            //);
        }

    }
}
