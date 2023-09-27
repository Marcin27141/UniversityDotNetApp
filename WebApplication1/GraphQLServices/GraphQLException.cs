using GraphQL;
using System;

namespace WebApplication1.GraphQLServices
{
    public class GraphQLException : Exception
    {
        public readonly GraphQLError[] errors;

        public GraphQLException(GraphQLError[] errors)
        {
            this.errors = errors;
        }
    }
}
