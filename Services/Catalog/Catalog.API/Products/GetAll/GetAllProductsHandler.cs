namespace Catalog.API.Products.GetAll
{
    internal record GetAllProductsQuery() : IQuery<GetAllProductsResult>;
    internal record GetAllProductsResult(IEnumerable<Product> Products);
    internal class GetAllProductsHandler(IDocumentSession session, ILogger<GetAllProductsHandler> logger) 
        : IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
    {
        public async Task<GetAllProductsResult> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all products with {@Query}", query);
            var res = await session.Query<Product>().ToListAsync(cancellationToken);
            logger.LogInformation("Response in get all products: " + res.ToString());
            return new GetAllProductsResult(res);
        }
    }
}
