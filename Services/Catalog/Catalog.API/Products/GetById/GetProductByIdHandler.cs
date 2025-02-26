namespace Catalog.API.Products.GetById
{
    internal record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    internal record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting product by id with {@Query}", request);
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken)
                ?? throw new ProductNotFoundException();
            logger.LogInformation("Response in get product by id: " + product.ToString());
            return new GetProductByIdResult(product);
        }
    }
}
