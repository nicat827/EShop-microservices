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
    internal class StoreBasketHandler : ICommandHandler<StoreBasketCommand>
    {
        public async Task<Unit> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
