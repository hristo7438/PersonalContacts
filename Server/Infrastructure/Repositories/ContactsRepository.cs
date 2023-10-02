namespace Infrastructure.Repositories
{
	using Application;
	using Application.Queries.Common;
	using Domain;
	using Domain.Models;
	using Infrastructure.Persistence;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	internal class ContactsRepository : DataRepository<IPersonalContactsDbContext, Contact>,
		IPersonalContactsDomainRepository,
		IPersonalContactsQueryRepository
	{
		public ContactsRepository(IPersonalContactsDbContext db)
			: base(db) { }

		public async Task<Contact> GetById(int id, CancellationToken cancellationToken)
		=> await this
				.All()
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

		public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
			=> await this
				.All()
				.AnyAsync(x => x.Id == id, cancellationToken);

		public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
		{
			var contact = await this.All().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

			if (contact == null)
			{
				return false;
			}

			this.Data.Contacts.Remove(contact);

			await this.Data.SaveChangesAsync(cancellationToken);

			return true;
		}

		public async Task<ContactOutputModel> GetDetails(int id, CancellationToken cancellationToken = default)
			=> await this
				.All()
				.Where(x => x.Id == id)
				.Select(x => ContactOutputModel.Create(x.Id, x.FirstName, x.SurName, x.Address, x.DateOfBirth, x.PhoneNumber, x.IBAN))
					.FirstOrDefaultAsync(cancellationToken);

		public async Task<IEnumerable<ContactOutputModel>> Search(CancellationToken cancellationToken = default)
			=> await this
				.All()
				.Select(x => ContactOutputModel.Create(x.Id, x.FirstName, x.SurName, x.Address, x.DateOfBirth, x.PhoneNumber, x.IBAN))
					.ToListAsync(cancellationToken);
	}
}
