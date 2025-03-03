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
    internal class StoreBasketHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand>
    {
        public async Task<Unit> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.StoreBasketAsync(command.Cart, cancellationToken);
            return Unit.Value;
        }
    }
}
