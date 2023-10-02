namespace Web
{
	using Application.Common;
	using FluentValidation;
	using FluentValidation.AspNetCore;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.DependencyInjection;

	public static class WebConfiguration
	{
		public static IServiceCollection AddWebComponents(this IServiceCollection services)
		{
			services
				.AddControllers()
				.AddNewtonsoftJson();

			services
				.AddEndpointsApiExplorer()
				.AddSwaggerGen()
				.AddMemoryCache()
				.AddFluentValidationAutoValidation()
				.AddValidatorsFromAssembly(typeof(Result).Assembly);

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			return services;
		}
	}
}