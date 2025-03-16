using FluentValidation;

namespace Discount.Grpc.Validators
{
    public class UpdateDiscountValidator : AbstractValidator<UpdateDiscountRequest>
    {
        public UpdateDiscountValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0)
                .WithMessage("Amount must be greater than zero");
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Id).GreaterThan(0)
                .WithMessage("Id must be greater than zero");
        }
    }
}
