import { baseUrl } from "./constants"

describe('contacts tests', () => {
  beforeEach(() => {
    cy.visit(baseUrl);
  });

  it('creates contact', () => {
    cy.get('button').contains('Add Contact').click();

    cy.get('p-dynamicdialog').should('be.visible');
    cy.get('.p-dialog-header').should('contain.text', 'Add contact');

    cy.get('input[formcontrolname="firstName"]').type("Hristo");
    cy.get('input[formcontrolname="surName"]').type("Hristov");
    cy.get('textarea[formcontrolname="address"]').type("Slavejkov street 1");
    cy.get('p-calendar[formcontrolname="dateOfBirth"]').type("08/05/1990");
    cy.get('input[formcontrolname="phoneNumber"]').type("0881111111");
    cy.get('input[formcontrolname="iban"]').type("BG80786128735618231231");

    cy.get('p-button[type="submit"]').click();

    cy.get('tr').last().within(() => {
      cy.contains("Hristo Hristov");
      cy.contains("Slavejkov street 1");
      cy.contains(new Date("08/05/1990").toISOString());
      cy.contains("0881111111");
      cy.contains("BG80786128735618231231");
    });
  });
});