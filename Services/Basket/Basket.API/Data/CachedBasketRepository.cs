
using JasperFx.Core;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    internal class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public async Task DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasketAsync(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var basketResult = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(basketResult))
                return JsonSerializer.Deserialize<ShoppingCart>(basketResult)!;
            var basket = await repository.GetBasketAsync(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasketAsync(cart, cancellationToken);
            await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), cancellationToken);
        }
    }
}
