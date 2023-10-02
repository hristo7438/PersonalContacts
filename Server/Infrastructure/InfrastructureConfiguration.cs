namespace Infrastructure
{
	using Application;
	using Domain;
	using Infrastructure.Persistence;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public static class InfrastructureConfiguration
	{
		public static IServiceCollection AddInfrastructure(
			this IServiceCollection services, IConfiguration configuration)
				=> services
					.AddDatabase(configuration)
					.AddRepositories();

		private static IServiceCollection AddDatabase(
			this IServiceCollection services, IConfiguration configuration)
				=> services
					.AddScoped<DbContext, PersonalContactsDbContext>()
					.AddDbContext<PersonalContactsDbContext>(options => options
						.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
					.AddScoped<IPersonalContactsDbContext>(provider => provider.GetService<PersonalContactsDbContext>());

		internal static IServiceCollection AddRepositories(this IServiceCollection services)
			=> services
				.Scan(scan => scan
					.FromCallingAssembly()
					.AddClasses(classes => classes
						.AssignableTo(typeof(IDomainRepository<>))
						.AssignableTo(typeof(IQueryRepository<>)))
					.AsImplementedInterfaces()
					.WithTransientLifetime());

	}
}