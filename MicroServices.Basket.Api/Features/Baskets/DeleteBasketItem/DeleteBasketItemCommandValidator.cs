using FluentValidation;
using MicroServices.Shared;

namespace MicroServices.Basket.Api.Features.Baskets.DeleteBasketItem
{
	public class DeleteBasketItemCommandValidator : AbstractValidator<DeleteBasketItemCommand>
	{
		public DeleteBasketItemCommandValidator()
		{
			RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId is required");
		}
	}
}
