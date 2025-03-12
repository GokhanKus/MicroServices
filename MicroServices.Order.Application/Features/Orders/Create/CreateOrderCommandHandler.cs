using MediatR;
using MicroServices.Order.Application.Contracts.Repositories;
using MicroServices.Order.Application.Contracts.UnitOfWorks;
using MicroServices.Order.Domain.Entities;
using MicroServices.Shared;
using MicroServices.Shared.Services;
using System.Net;

namespace MicroServices.Order.Application.Features.Orders.Create
{
	public class CreateOrderCommandHandler(IOrderRepository orderRepo, IIdentityService identityService, IUnitOfWork unitOfWork)
		: IRequestHandler<CreateOrderCommand, ServiceResult>
	{
		public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			if (request.OrderItems is null || !request.OrderItems.Any())
				return ServiceResult.Error("order items not found", "in order must have at least one order item", HttpStatusCode.BadRequest);

			var newAddress = new Address
			{
				Province = request.Address.Province,
				District = request.Address.District,
				Street = request.Address.Street,
				ZipCode = request.Address.ZipCode,
				Line = request.Address.Line
			};

			//TODO: newAddress.Id degeri geliyor mu?
			var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.UserId, request.DiscountRate, newAddress.Id);

			#region Transaction & Address Id
			//Order entitysinin icinde Address navigation prop'u var bu yüzden address id otomatik olarak set edilecek aksi taktirde
			//address eklendikten sonra veri tabanına kaydedilip, id'si alınıp CreateUnPaidOrder() metodunda adres id parametre olarak gecilecekti
			//ve bunun icinde bir transaction gerekiyordu
			#endregion
			order.Address = newAddress;

			foreach (var item in request.OrderItems)
				order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice);

			orderRepo.Add(order);
			await unitOfWork.CommitAsync(cancellationToken);

			var paymentId = Guid.Empty;
			//TODO: Payment işlemler başlayacak
			order.SetPaidStatus(paymentId);

			orderRepo.Update(order);
			await unitOfWork.CommitAsync(cancellationToken);

			return ServiceResult.SuccessAsNoContent();
		}
	}
}
