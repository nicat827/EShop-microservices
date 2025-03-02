namespace Basket.API.Basket.Get
{
    internal record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    internal record GetBasketResult(ShoppingCart Cart);
    internal class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
