namespace Application
{
	using Application.Queries.Common;
	using Domain.Models;
	using System.Collections.Generic;

	public interface IPersonalContactsQueryRepository : IQueryRepository<Contact>
	{
		Task<ContactOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

		Task<IEnumerable<ContactOutputModel>> Search(CancellationToken cancellationToken = default);
	}
}
