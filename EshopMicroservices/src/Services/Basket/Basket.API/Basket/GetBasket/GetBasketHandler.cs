using Basket.API.Basket.Data;

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetbasketResult>;
public record GetbasketResult(ShoppingCart Cart);
public class GetBasketQueryHandler(IBasketRepository _repository) : IQueryHandler<GetBasketQuery, GetbasketResult>
{
    public async Task<GetbasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        //TODO: get basket from DB
        var basket = await _repository.GetBasket(query.UserName);
        return new GetbasketResult(basket);
    }
}
