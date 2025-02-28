
namespace Catalog.API.Products.Delete
{
    internal record DeleteProductCommand(Guid Id) : ICommand;
    internal class DeleteProductHandler(IDocumentSession session)
        : ICommandHandler<DeleteProductCommand>
    {
        public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            Product product = await session.LoadAsync<Product>(command.Id, cancellationToken)
                ?? throw new ProductNotFoundException(command.Id);
            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
