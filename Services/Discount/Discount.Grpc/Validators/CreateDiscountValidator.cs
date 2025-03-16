using FluentValidation;

namespace Discount.Grpc.Validators
{
    public class CreateDiscountValidator : AbstractValidator<CreateDiscountRequest>
    {
        public CreateDiscountValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0)
                .WithMessage("Amount must be greater than zero");
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
        }
    }
}
