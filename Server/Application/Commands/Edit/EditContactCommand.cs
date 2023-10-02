namespace Application.Commands.Edit
{
	using Application.Commands.Common;
	using Application.Common;
	using Domain;
	using MediatR;

	public class EditContactCommand : ContactCommand<EditContactCommand>, IRequest<Result>
	{
		public class EditContactCommandHandler : IRequestHandler<EditContactCommand, Result>
		{
			private readonly IPersonalContactsDomainRepository _personalContactsDomainRepository;

			public EditContactCommandHandler(
				IPersonalContactsDomainRepository personalContactsDomainRepository)
				=> this._personalContactsDomainRepository = personalContactsDomainRepository;

			public async Task<Result> Handle(
				EditContactCommand request,
				CancellationToken cancellationToken)
			{
				var contactExists = await this._personalContactsDomainRepository.Exists(request.Id, cancellationToken);

				if (!contactExists)
				{
					return contactExists;
				}

				var contact = await this._personalContactsDomainRepository
					.GetById(request.Id, cancellationToken);

				contact
					.UpdateName(request.FirstName, request.SurName)
					.UpdateAddress(request.Address)
					.UpdateDateOfBIrth(request.DateOfBirth)
					.UpdatePhoneNumber(request.PhoneNumber)
					.UpdateIBAN(request.IBAN);

				await this._personalContactsDomainRepository.Save(contact, cancellationToken);

				return Result.Success;
			}
		}
	}
}
