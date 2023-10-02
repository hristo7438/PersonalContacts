namespace Infrastructure.Persistence
{
	using Domain.Models;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
	using System.Reflection;

	internal class PersonalContactsDbContext : DbContext,
		IPersonalContactsDbContext
	{
		private readonly Stack<object> savesChangesTracker;

		public PersonalContactsDbContext(DbContextOptions<PersonalContactsDbContext> options)
			: base(options)
		{
			this.savesChangesTracker = new Stack<object>();
		}

		public DbSet<Contact> Contacts { get; set; } = default!;

		protected Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}
	}
}
