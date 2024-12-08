using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService
        (DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var couppon = await dbContext.Couppons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (couppon is null)
            {
                couppon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            }

            logger.LogInformation("Discount is retrived for ProductName: {productName}", couppon.ProductName);

            var couponModel = couppon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
            }

            dbContext.Couppons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;

        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
            }

            dbContext.Couppons.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var couppon = await dbContext.Couppons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (couppon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Discount with ProductName={request.ProductName} is not found"));
            }

            dbContext.Couppons.Remove(couppon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully deleted. ProductName: {productName}", couppon.ProductName);

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
