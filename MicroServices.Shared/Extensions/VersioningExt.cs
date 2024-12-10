using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServices.Shared.Extensions
{
	public static class VersioningExt
	{
		public static IServiceCollection AddVersioningExt(this IServiceCollection services)
		{
			services.AddApiVersioning(options =>
			{
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ReportApiVersions = true;
				options.ApiVersionReader = new UrlSegmentApiVersionReader(); //version bilgisini url, header ve query'de alabiliriz, biz url'de alacagiz

			}).AddApiExplorer(options => //ApiExplorer swagger icin yaptigimiz konfigurasyon
			{
				options.GroupNameFormat = "'v'V";
				options.SubstituteApiVersionInUrl = true;
			});
			return services;
		}

		//minimal api icin yaptigimiz conf., mvc desing pattern olsaydi daha farkli yapacaktik
		public static ApiVersionSet AddVersionSetExt(this WebApplication app)
		{
			var apiVersionSet = app.NewApiVersionSet()
			   .HasApiVersion(new ApiVersion(1, 0))
			   .HasApiVersion(new ApiVersion(1, 2))
			   .HasApiVersion(new ApiVersion(2, 0))
			   .ReportApiVersions()
			   .Build();
			return apiVersionSet;
		}
	}
}
