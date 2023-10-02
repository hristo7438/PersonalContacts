namespace Application.Commands.Create
{
	using Application.Commands.Common;
	using Domain;
	using Domain.Factories;
	using MediatR;

	public class CreateContactCommand : ContactCommand<CreateContactCommand>, IRequest<CreateContactOutputModel>
	{
		public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, CreateContactOutputModel>
		{
			private readonly IPersonalContactsDomainRepository _personalContactsDomainRepository;
			private readonly IContactFactory _contactFactory;

			public CreateContactCommandHandler(
				IPersonalContactsDomainRepository personalContactsDomainRepository,
				IContactFactory contactFactory)
			=> (this._personalContactsDomainRepository, this._contactFactory) = (personalContactsDomainRepository, contactFactory);

			public async Task<CreateContactOutputModel> Handle(
				CreateContactCommand request,
				CancellationToken cancellationToken)
			{
				var contact = this._contactFactory
					.WithFirstName(request.FirstName)
					.WithSurName(request.SurName)
					.WithAddress(request.Address)
					.WithDateOfBirth(request.DateOfBirth)
					.WithPhoneNumber(request.PhoneNumber)
					.WithIBAN(request.IBAN)
					.Build();

				await this._personalContactsDomainRepository.Save(contact, cancellationToken);

				return new CreateContactOutputModel(contact.Id);
			}
		}
	}
}
