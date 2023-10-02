namespace Domain
{
	using Domain.Models;
	using System.Threading;
	using System.Threading.Tasks;

	public interface IPersonalContactsDomainRepository : IDomainRepository<Contact>
	{
		Task<Contact> GetById(int id, CancellationToken cancellationToken = default);

		Task<bool> Exists(int id, CancellationToken cancellationToken = default);

		Task<bool> Delete(int id, CancellationToken cancellationToken = default);
	}
}
