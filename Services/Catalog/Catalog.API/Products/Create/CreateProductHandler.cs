namespace Catalog.API.Products.Create
{
    internal record CreateProductCommand(string Name, IEnumerable<string> Categories, string Description, decimal Price) 
        : ICommand<CreateProductResult>;
    internal record CreateProductResult(Guid Id);

    internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Categories = request.Categories
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}
