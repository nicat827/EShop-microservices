using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext context, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly DiscountContext _context = context;

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            if (request is null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Request is required"));

            if (await _context.Coupons.AnyAsync(c => c.ProductName == request.ProductName))
                throw new RpcException(new Status(StatusCode.AlreadyExists, "Discount already exists"));
            Coupon newCoupon = new Coupon
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Amount = request.Amount
            };
            await _context.Coupons.AddAsync(newCoupon);
            await _context.SaveChangesAsync();
            logger.LogInformation($"Discount is successfully created. ProductName: {newCoupon.ProductName}");
            return newCoupon.Adapt<CouponModel>();
        }
        public override async Task<Empty> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            Coupon coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName)
                ?? throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            logger.LogInformation($"Discount is successfully deleted. ProductName: {request.ProductName}");
            return new Empty();
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
            => await _context.Coupons
            .Where(c => c.ProductName == request.ProductName)
            .Select(c => new CouponModel
            {
                Id = c.Id,
                ProductName = c.ProductName,
                Description = c.Description,
                Amount = c.Amount
            })
            .FirstOrDefaultAsync() ?? throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));


        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            Coupon coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName)
                ?? throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
            coupon.ProductName = request.ProductName;
            coupon.Description = request.Description;
            coupon.Amount = request.Amount;
            await _context.SaveChangesAsync();
            logger.LogInformation($"Discount is successfully updated. ProductName: {coupon.ProductName}");
            return coupon.Adapt<CouponModel>();
        }
    }
}
