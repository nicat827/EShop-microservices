using FluentValidation;

namespace Discount.Grpc.Validators
{
    public class GetDiscountValidator : AbstractValidator<GetDiscountRequest>
    {
        public GetDiscountValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
        }
    }
}
