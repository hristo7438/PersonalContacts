namespace Application.Queries.Search
{
	using Application.Common;
	using Application.Queries.Common;
	using MediatR;
	using System.Collections.Generic;

	public class ContactsSearchQuery : EntityCommand<int>, IRequest<IEnumerable<ContactOutputModel>>
	{
		public class ContactsSearchQueryHandler : IRequestHandler<ContactsSearchQuery, IEnumerable<ContactOutputModel>>
		{
			private readonly IPersonalContactsQueryRepository _personalContactsQueryRepository;

			public ContactsSearchQueryHandler(
				IPersonalContactsQueryRepository personalContactsQueryRepository)
				=> this._personalContactsQueryRepository = personalContactsQueryRepository;

			public async Task<IEnumerable<ContactOutputModel>> Handle(
				ContactsSearchQuery request,
				CancellationToken cancellationToken)
			{
				var contacts = await this._personalContactsQueryRepository.Search(
					cancellationToken);

				return contacts;
			}
		}
	}
}
