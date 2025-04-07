using Discount.Grpc;

namespace Basket.API.Basket.Store
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand;
    public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketValidator()
        {
            RuleFor(x => x.Cart).NotEmpty().WithMessage("Cart is required");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    internal class StoreBasketHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProtoService, ILogger<StoreBasketHandler> logger)
        : ICommandHandler<StoreBasketCommand>
    {
        public async Task<Unit> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            foreach (var item in command.Cart.Items)
            {
                try
                {
                    var coupon = await discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
                    item.Price -= coupon.Amount;
                }
                catch (Exception ex)
                {
                    logger.LogError("Failed to get discount. Error: {Message}", ex.Message);
                }
            }
            await repository.StoreBasketAsync(command.Cart, cancellationToken);
            return Unit.Value;
        }
    }
}
