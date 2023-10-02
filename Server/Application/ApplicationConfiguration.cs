using Application.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
	public static class ApplicationConfiguration
	{
		public static IServiceCollection AddApplication(
			this IServiceCollection services)
			=> services
				.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
	}
}