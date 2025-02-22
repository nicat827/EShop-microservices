namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public IEnumerable<string> Categories { get; set; } = new List<string>();
        public decimal Price { get; set; }

    }
}
