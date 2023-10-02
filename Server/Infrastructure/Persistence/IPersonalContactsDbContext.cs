namespace Infrastructure.Persistence
{
	using Domain.Models;
	using Microsoft.EntityFrameworkCore;

	internal interface IPersonalContactsDbContext : IDbContext
	{
		DbSet<Contact> Contacts { get; }
	}
}
