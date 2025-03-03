namespace Basket.API.Basket.Get
{
    internal record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    internal record GetBasketResult(ShoppingCart Cart);
    internal class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasketAsync(request.UserName, cancellationToken);
            return new GetBasketResult(basket);
        }
    }
}
