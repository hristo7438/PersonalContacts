namespace Application.Commands.Delete
{
	using Application.Common;
	using Domain;
	using MediatR;

	public class DeleteContactCommand : EntityCommand<int>, IRequest<Result>
	{
		public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Result>
		{
			private readonly IPersonalContactsDomainRepository _personalContactsDomainRepository;

			public DeleteContactCommandHandler(
				IPersonalContactsDomainRepository personalContactsDomainRepository)
				=> this._personalContactsDomainRepository = personalContactsDomainRepository;

			public async Task<Result> Handle(
				DeleteContactCommand request,
				CancellationToken cancellationToken)
			{
				var contactExists = await this._personalContactsDomainRepository.Exists(request.Id, cancellationToken);

				if (!contactExists)
				{
					return contactExists;
				}

				return await this._personalContactsDomainRepository.Delete(
					request.Id,
					cancellationToken);
			}
		}
	}
}
