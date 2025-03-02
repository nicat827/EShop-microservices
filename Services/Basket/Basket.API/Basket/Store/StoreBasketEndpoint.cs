namespace Basket.API.Basket.Store
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/{userName}", async (string userName, StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();
                var result = await sender.Send(command);
                return Results.Created();
            })
                .WithName("StoreBasket")
                .Produces(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Store basket by user name")
                .WithSummary("Store basket by user name");
        }
    }
}
