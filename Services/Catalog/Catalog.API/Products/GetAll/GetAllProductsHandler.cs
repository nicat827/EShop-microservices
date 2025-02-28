using Marten.Pagination;

namespace Catalog.API.Products.GetAll
{
    internal record GetAllProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetAllProductsResult>;
    internal record GetAllProductsResult(IEnumerable<Product> Products);
    internal class GetAllProductsHandler(IDocumentSession session) 
        : IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
    {
        public async Task<GetAllProductsResult> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var res = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10,cancellationToken);
            return new GetAllProductsResult(res);
        }
    }
}
