using MicroServices.Shared.Services;

namespace MicroServices.Discount.Api.Features.Discounts.Create
{
	public class CreateDiscountCommandHandler(AppDbContext context) : IRequestHandler<CreateDiscountCommand, ServiceResult>
	{
		public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
		{
			var hasCodeForUser = await context.Discounts.AnyAsync(d => d.UserId == request.UserId && d.Code == request.Code, cancellationToken);

			if (hasCodeForUser)
				return ServiceResult.Error("Discount code has already exists for this user", HttpStatusCode.BadRequest);

			var discount = new Repositories.Discount
			{
				Id = NewId.NextSequentialGuid(),
				Code = request.Code,
				Created = DateTime.Now, //global proje olacaksa utcnow..
				Rate = request.Rate,
				Expired = request.Expired,
				UserId = request.UserId
			};
			await context.Discounts.AddAsync(discount);
			//async operasyonu durdurmanin tek yolu cancellationtokeni geçmektir boylelikle async operasyonda exception fırlatarak islemin devam etmesini engeller
			await context.SaveChangesAsync(cancellationToken);
			return ServiceResult.SuccessAsNoContent();
		}
	}
}
