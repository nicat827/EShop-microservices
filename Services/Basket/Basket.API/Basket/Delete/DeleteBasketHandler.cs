namespace Basket.API.Basket.Delete
{
    public record DeleteBasketCommand(string UserName) : ICommand;
    public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required!");
        }
    }
    internal class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand>
    {
        public async Task<Unit> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteBasketAsync(request.UserName, cancellationToken);
            return Unit.Value;
        }
    }
}
