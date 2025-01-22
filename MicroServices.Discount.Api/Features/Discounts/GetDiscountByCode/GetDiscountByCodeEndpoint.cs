﻿using Microsoft.AspNetCore.Mvc;

namespace MicroServices.Discount.Api.Features.Discounts.GetDiscountByCode
{
	public static class GetDiscountByCodeEndpoint
	{
		public static RouteGroupBuilder GetDiscountByCodeGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapGet("/{code:length(10)}",//route constraint
				 async (string code, IMediator mediator) =>
				 (await mediator.Send(new GetDiscountByCodeQuery(code))).ToGenericResult())
				.WithName("GetDiscountByCode")
				.MapToApiVersion(1, 0)
				.Produces<GetDiscountByCodeQueryResponse>(StatusCodes.Status200OK)
				.Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
				.Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

			return group;
		}
	}
}
