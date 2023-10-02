namespace Application.Queries.Common
{
	using System;

	public class ContactOutputModel
	{
		public static ContactOutputModel Create(int id, string firstName, string surName,
			string address, DateTime dateOfBirth, string phoneNumber, string iban)
			=> new ContactOutputModel()
			{
				Id = id,
				FirstName = firstName,
				SurName = surName,
				Address = address,
				DateOfBirth = dateOfBirth,
				PhoneNumber = phoneNumber,
				IBAN = iban
			};

		private ContactOutputModel() { }

		public int Id { get; private set; }

		public string FirstName { get; set; } = default!;

		public string SurName { get; set; } = default!;

		public string Address { get; set; } = default!;

		public DateTime DateOfBirth { get; set; } = default!;

		public string PhoneNumber { get; set; } = default!;

		public string IBAN { get; set; } = default!;
	}
}