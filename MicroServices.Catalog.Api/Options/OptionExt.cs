namespace MicroServices.Catalog.Api.Options
{
	public static class OptionExt
	{
		public static IServiceCollection AddOptionsExt(this IServiceCollection services)
		{
			services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations().ValidateOnStart();
			return services;	
		}
	}
}
