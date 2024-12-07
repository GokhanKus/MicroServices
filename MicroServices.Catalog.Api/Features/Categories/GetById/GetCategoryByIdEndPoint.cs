﻿using AutoMapper;
using MediatR;
using MicroServices.Catalog.Api.Features.Categories.Dtos;
using MicroServices.Catalog.Api.Repositories;
using MicroServices.Shared;
using MicroServices.Shared.Extensions;
using System.Net;

namespace MicroServices.Catalog.Api.Features.Categories.GetById
{
	public record GetCategoryByIdQuery(Guid Id) : IRequest<ServiceResult<CategoryDto>>;

	public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
	{
		public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
		{
			var category = await context.Categories.FindAsync(request.Id, cancellationToken);

			if (category == null)
			{
				return ServiceResult<CategoryDto>.Error("Category not found",
					$"The category with id({request.Id}) was not found", HttpStatusCode.NotFound);
			}

			var categoriesAsDto = mapper.Map<CategoryDto>(category);
			return ServiceResult<CategoryDto>.SuccessAsOk(categoriesAsDto);
		}
	}

	public static class GetCategoryByIdEndPoint
	{
		public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapGet("/{id:guid}", async (Guid id, IMediator mediator)
				=> (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult());

			return group;
		}
	}
}