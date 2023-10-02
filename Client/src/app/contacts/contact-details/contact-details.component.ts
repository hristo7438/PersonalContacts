import { Component } from '@angular/core';
import { Contact } from 'src/app/models/contact';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-contact-details',
  templateUrl: './contact-details.component.html',
  styleUrls: ['./contact-details.component.scss']
})
export class ContactDetailsComponent {
  public contactForm: FormGroup;
  public contact: Contact;

  constructor(
    private readonly _dialogRef: DynamicDialogRef,
    private readonly _dialogConfig: DynamicDialogConfig,
    private _formBuilder: FormBuilder) {
    this.contact = _dialogConfig.data.contact;
    this.contactForm = this._buildForm(this.contact);
  }

  public save() {
    this.contact = { id: this.contact?.id, ...this.contactForm.value, dateOfBirth: new Date(this.dateOfBirth?.value).toISOString() };
    this._dialogRef.close(this.contact);
  }

  public cancel() {
    this._dialogRef.close();
  }

  private _buildForm(contact: Contact | null): FormGroup {
    return this._formBuilder.group({
      firstName: [contact?.firstName, Validators.required],
      surName: [contact?.surName, Validators.required],
      address: [contact?.address, Validators.required],
      dateOfBirth: [contact?.dateOfBirth, Validators.required],
      phoneNumber: [contact?.phoneNumber, Validators.required],
      iban: [contact?.iban, [Validators.required, Validators.pattern('^[A-Z]{2}(?:[ ]?[0-9]){18,20}$')]],
    });
  }

  get firstName() {
    return this.contactForm.get('firstName');
  }

  get surName() {
    return this.contactForm.get('surName');
  }

  get address() {
    return this.contactForm.get('address');
  }

  get dateOfBirth() {
    return this.contactForm.get('dateOfBirth');
  }

  get phoneNumber() {
    return this.contactForm.get('phoneNumber');
  }

  get iban() {
    return this.contactForm.get('iban');
  }
}
