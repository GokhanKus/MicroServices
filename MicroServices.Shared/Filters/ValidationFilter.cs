using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServices.Shared.Filters
{
	public class ValidationFilter<T> : IEndpointFilter
	{
		public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
		{
			//asagidaki validator'den, AbstractValidator sınıfını miras alan bir sınıf var mı yok mu onu kontrol ediyoruz
			var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
			if (validator is null)
			{
				//endpointteki handler calisabilir yani CreateCategoryEndpoint.csteki CreateCategoryGroupItemEndpoint metodumuz
				return await next(context);
			}

			var requestModel = context.Arguments.OfType<T>().FirstOrDefault(); //ornegin T CreateCategoryCommand command ise buna bakacak var mı yok mu?

			if (requestModel is null)
				return await next(context);

			var validateResult = await validator.ValidateAsync(requestModel);

			if (!validateResult.IsValid)
				return Results.ValidationProblem(validateResult.ToDictionary()); //valid degilse error mesajlarini yazdiralim

			return await next(context);
		}
	}
}
