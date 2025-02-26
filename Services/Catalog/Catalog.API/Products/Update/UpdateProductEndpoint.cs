
namespace Catalog.API.Products.Update
{
    public record UpdateProductRequest(Guid Id,string Name, List<string> Categories, string Description, decimal Price);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                await sender.Send(command);
                return Results.NoContent();
            })
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithDescription("Update product")
                .WithSummary("Update product");
        }
    }
}
