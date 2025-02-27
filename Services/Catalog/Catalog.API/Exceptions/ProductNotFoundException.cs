namespace Catalog.API.Exceptions
{
    internal class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException() : base("Product not found") { }
        public ProductNotFoundException(Guid Id) : base("Product", Id) { }
        
    }
}
