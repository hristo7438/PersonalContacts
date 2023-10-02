using Application.Commands.Create;
using Application.Commands.Edit;
using Application.Queries.Common;

namespace Tests
{
	[Trait("Category", "Integration")]
	public class ContactsTests : IClassFixture<WebApplicationFactory<Program>>
	{
		protected readonly WebApplicationFactory<Program> _factory;
		protected readonly HttpClient _client;

		public ContactsTests(WebApplicationFactory<Program> factory)
			=> (this._factory, this._client) = (factory, factory.CreateClient());

		[Fact]
		public async Task Should_confirm_contacts_crud()
		{
			CreateContactCommand createRequest = new CreateContactCommand()
			{
				FirstName = "Hristo",
				SurName = "Hristov",
				Address = "Slavejkov street 1",
				DateOfBirth = new DateTime(1990, 8, 4),
				PhoneNumber = "08811221122",
				IBAN = "BG80786128735618231231"
			};

			using var createResponseMsg = await _client.PostAsJsonAsync("/contacts", createRequest);

			CreateContactOutputModel createResponse
				= JsonConvert.DeserializeObject<CreateContactOutputModel>(await createResponseMsg.Content?.ReadAsStringAsync());

			createResponse.Should().NotBeNull();
			createResponse!.Id.Should().BeGreaterThanOrEqualTo(0);

			ContactOutputModel newContact
				= JsonConvert.DeserializeObject<ContactOutputModel>(await _client.GetStringAsync($"/contacts/{createResponse.Id}"));

			newContact.Should().NotBeNull();
			newContact!.FirstName.Should().Be(createRequest.FirstName);
			newContact.SurName.Should().Be(createRequest.SurName);
			newContact.Address.Should().Be(createRequest.Address);
			newContact.DateOfBirth.Should().Be(createRequest.DateOfBirth);
			newContact.PhoneNumber.Should().Be(createRequest.PhoneNumber);
			newContact.IBAN.Should().Be(createRequest.IBAN);

			EditContactCommand updateRequest = new EditContactCommand()
			{
				Id = createResponse.Id,
				FirstName = newContact.FirstName,
				SurName = newContact.SurName,
				PhoneNumber = "08812332123",
				Address = newContact.Address,
				DateOfBirth = newContact.DateOfBirth,
				IBAN = newContact.IBAN
			};

			using var updateResponseMsg = await _client.PutAsJsonAsync($"/contacts/{updateRequest.Id}", updateRequest);

			updateResponseMsg.IsSuccessStatusCode.Should().Be(true);

			ContactOutputModel updatedContact
				= JsonConvert.DeserializeObject<ContactOutputModel>(await _client.GetStringAsync($"/contacts/{updateRequest.Id}"));

			updatedContact!.FirstName.Should().Be(updateRequest.FirstName);
			updatedContact.SurName.Should().Be(updateRequest.SurName);
			updatedContact.Address.Should().Be(updateRequest.Address);
			updatedContact.DateOfBirth.Should().Be(updateRequest.DateOfBirth);
			updatedContact.PhoneNumber.Should().Be(updateRequest.PhoneNumber);
			updatedContact.IBAN.Should().Be(updateRequest.IBAN);

			using var deleteResponseMsg = await _client.DeleteAsync($"/contacts/{createResponse.Id}");

			deleteResponseMsg.IsSuccessStatusCode.Should().Be(true);

			List<ContactOutputModel> allContactsResponse
				= JsonConvert.DeserializeObject<List<ContactOutputModel>>(await _client.GetStringAsync("/contacts"));

			allContactsResponse.Should().NotContain(c => c.Id == createResponse.Id);
		}
	}
}