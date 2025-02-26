
namespace Catalog.API.Products.Update
{
    internal record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, decimal Price) : ICommand;
    internal class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger)
        : ICommandHandler<UpdateProductCommand>
    {
        public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating product with {@Command}", command);
            Product product = await session.LoadAsync<Product>(command.Id, cancellationToken)
                ?? throw new ProductNotFoundException();

            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Categories = command.Categories;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
