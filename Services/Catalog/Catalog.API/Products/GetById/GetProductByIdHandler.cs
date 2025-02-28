namespace Catalog.API.Products.GetById
{
    internal record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    internal record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler(IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken)
                ?? throw new ProductNotFoundException(request.Id);
            return new GetProductByIdResult(product);
        }
    }
}
