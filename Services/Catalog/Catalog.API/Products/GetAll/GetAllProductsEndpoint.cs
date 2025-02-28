namespace Catalog.API.Products.GetAll
{
    internal record GetAllProductsRequest(int? PageNumber = 1, int? PageSize = 10);
    internal record GetAllProductsResponse(IEnumerable<Product> Products);
    public class GetAllProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetAllProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetAllProductsQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<GetAllProductsResponse>();
                return Results.Ok(response);
            })
                .WithName("GetAllProducts")
                .Produces<GetAllProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Get all products")
                .WithSummary("Get all products");
        }
    }
}
