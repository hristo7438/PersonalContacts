namespace Application.Queries.Details
{
	using Application.Common;
	using Application.Queries.Common;
	using MediatR;

	public class ContactDetailsQuery : EntityCommand<int>, IRequest<ContactOutputModel>
	{
		public class ContactDetailsQueryHandler : IRequestHandler<ContactDetailsQuery, ContactOutputModel>
		{
			private readonly IPersonalContactsQueryRepository _personalContactsQueryRepository;

			public ContactDetailsQueryHandler(
				IPersonalContactsQueryRepository personalContactsQueryRepository)
				=> this._personalContactsQueryRepository = personalContactsQueryRepository;

			public async Task<ContactOutputModel> Handle(
				ContactDetailsQuery request,
				CancellationToken cancellationToken)
			{
				var contactDetails = await this._personalContactsQueryRepository.GetDetails(
					request.Id,
					cancellationToken);

				return contactDetails;
			}
		}
	}
}
