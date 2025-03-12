using MediatR;
using MicroServices.Order.Application.Contracts.Repositories;
using MicroServices.Order.Domain.Entities;
using MicroServices.Shared;
using MicroServices.Shared.Services;
using System.Net;

namespace MicroServices.Order.Application.Features.Orders.Create
{
	public class CreateOrderCommandHandler(IGenericRepository<Guid, Domain.Entities.Order> orderRepo, IGenericRepository<int, Address> addressRepo, IIdentityService identityService)
		: IRequestHandler<CreateOrderCommand, ServiceResult>
	{
		public Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			if(request.OrderItems is null || !request.OrderItems.Any())
			{
				return Task.FromResult(ServiceResult.Error("order items not found","in order must have at least one order item",HttpStatusCode.BadRequest));
			}
			//TODO: begin transaction
			var newAddress = new Address
			{
				Province = request.Address.Province,
				District = request.Address.District,
				Street = request.Address.Street,
				ZipCode = request.Address.ZipCode,
				Line = request.Address.Line
			};
			addressRepo.Add(newAddress);
			//TODO: unitOfWork.Commit()

			var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.UserId, request.DiscountRate, newAddress.Id);

			foreach (var item in request.OrderItems)
			{
				order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice);
			}
			orderRepo.Add(order);

			var paymentId = Guid.Empty;
			//TODO: Payment işlemler başlayacak
			order.SetPaidStatus(paymentId);

			orderRepo.Update(order);
			//unitOfWork.Commit()
			return Task.FromResult(ServiceResult.SuccessAsNoContent());
		}
	}
}
