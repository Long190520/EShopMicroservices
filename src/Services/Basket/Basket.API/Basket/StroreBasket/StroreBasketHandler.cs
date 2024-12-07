namespace Basket.API.Basket.StroreBasket
{
    public record StroreBasketCommand(ShoppingCart Cart) 
        : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StroreBasketCommandValidator : AbstractValidator<StroreBasketCommand>
    {
        public StroreBasketCommandValidator()
        {
            RuleFor(x=>x.Cart).NotNull().WithMessage("Cart cannot be null!");
            RuleFor(x=>x.Cart.UserName).NotEmpty().WithMessage("UserName is required!");
        }
    }
    public class StroreBasketCommandHandler
        (IBasketRepository repository)
        : ICommandHandler<StroreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StroreBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.StoreBasket(command.Cart, cancellationToken);
            //Update cache

            return new StoreBasketResult(command.Cart.UserName);

        }
    }
}
