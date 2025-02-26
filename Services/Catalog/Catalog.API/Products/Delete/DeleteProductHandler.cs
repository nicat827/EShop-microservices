
namespace Catalog.API.Products.Delete
{
    internal record DeleteProductCommand(Guid Id) : ICommand;
    internal class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger)
        : ICommandHandler<DeleteProductCommand>
    {
        public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductHandler called with command {@Command}", command);
            Product product = await session.LoadAsync<Product>(command.Id, cancellationToken)
                ?? throw new ProductNotFoundException();
            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
