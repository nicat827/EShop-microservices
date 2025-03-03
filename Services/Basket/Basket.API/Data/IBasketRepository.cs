namespace Basket.API.Data
{
    internal interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default);
        Task StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default);
        Task DeleteBasketAsync(string userName, CancellationToken cancellationToken = default);
    }
}
