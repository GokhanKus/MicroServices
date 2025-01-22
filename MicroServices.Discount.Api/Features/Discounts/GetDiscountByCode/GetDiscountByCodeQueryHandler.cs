

using MicroServices.Shared.Services;

namespace MicroServices.Discount.Api.Features.Discounts.GetDiscountByCode
{
	public class GetDiscountByCodeQueryHandler(AppDbContext context): IRequestHandler<GetDiscountByCodeQuery, ServiceResult<GetDiscountByCodeQueryResponse>>
	{
		public async Task<ServiceResult<GetDiscountByCodeQueryResponse>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
		{
			//single firstten farkli olarak eger eslesen birden fazla kayit varsa hata firlatir
			var hasDiscount = await context.Discounts.SingleOrDefaultAsync(x => x.Code == request.Code, cancellationToken);

			if (hasDiscount is null)
				return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount not found", HttpStatusCode.BadRequest);

			if (hasDiscount.Expired < DateTime.Now) //indirimin suresi dolmussa
				return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount is expired", HttpStatusCode.NotFound);

			return ServiceResult<GetDiscountByCodeQueryResponse>.SuccessAsOk(new GetDiscountByCodeQueryResponse(hasDiscount.Code, hasDiscount.Rate));
		}
	}
}
