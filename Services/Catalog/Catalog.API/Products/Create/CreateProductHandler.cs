using Shared.CQRS;

namespace Catalog.API.Products.Create
{
    internal record CreateProductCommand(string Name, IEnumerable<string> Categories, string Description, decimal Price) 
        : ICommand<CreateProductResult>;
    internal record CreateProductResult(Guid Id);

    internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
