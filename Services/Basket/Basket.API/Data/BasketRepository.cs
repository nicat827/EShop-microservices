

namespace Basket.API.Data
{
    internal class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancellationToken);
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default) =>
            await session.LoadAsync<ShoppingCart>(userName, cancellationToken)
                ?? throw new BasketNotFoundException(userName);
        public async Task StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            session.Store(cart);
            await session.SaveChangesAsync(cancellationToken);
        }
    }
}
