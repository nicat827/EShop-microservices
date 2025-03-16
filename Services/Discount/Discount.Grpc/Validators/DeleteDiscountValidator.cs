using FluentValidation;

namespace Discount.Grpc.Validators
{
    public class DeleteDiscountValidator : AbstractValidator<DeleteDiscountRequest>
    {
        public DeleteDiscountValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
        }
    }
}
