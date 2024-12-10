namespace MicroServices.Catalog.Api.Features.Categories.GetAll
{
	public record GetAllCategoriesQuery : IRequestByServiceResult<List<CategoryDto>>;

	public class GetAllCategoryQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, ServiceResult<List<CategoryDto>>>
	{
		public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
		{
			var categories = await context.Categories.ToListAsync(cancellationToken);
			//var categoriesAsDto = categories.Select(c=> new CategoryDto(c.Id,c.Name)).ToList(); 
			var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
			return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesAsDto);
		}
	}

	public static class GetAllCategoriesEndpoint
	{
		public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapGet("/", async (IMediator mediator)
				=> (await mediator.Send(new GetAllCategoriesQuery())).ToGenericResult())
				.WithName("GetAllCategory")
				.MapToApiVersion(1, 0);

			return group;
		}
	}
}
