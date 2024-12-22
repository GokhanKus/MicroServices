using FluentValidation;
using FluentValidation.AspNetCore;
using MicroServices.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServices.Shared.Extensions
{
	public static class CommonServiceExt
	{
		public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
		{
			services.AddHttpContextAccessor();
			services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));

			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssemblyContaining(assembly);
			services.AddAutoMapper(assembly);

			services.AddScoped<IIdentityService, IdentityServiceFake>();
			return services;
		}
	}
}
