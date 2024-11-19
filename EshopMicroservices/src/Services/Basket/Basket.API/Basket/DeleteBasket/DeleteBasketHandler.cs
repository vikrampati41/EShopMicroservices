
namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketResultValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketResultValidator()
    {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class DeleteBasketCommandHandler(IBasketRepository _repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        //TODO: Delete basket from db and cache
        //session.Delete<Product>(command.Id);
        await _repository.DeleteBasket(command.UserName, cancellationToken);
        return new DeleteBasketResult(true);
    }
}
