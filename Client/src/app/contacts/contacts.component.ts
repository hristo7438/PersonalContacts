import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../state/app.state';
import { loadContacts } from '../state/contacts/contact.actions';
import { selectAllContacts } from '../state/contacts/contacts.selectors';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ContactDetailsComponent } from './contact-details/contact-details.component';
import { Contact } from '../models/contact';
import * as contactActions from "../state/contacts/contact.actions";
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss']
})
export class ContactsComponent implements OnInit, OnDestroy {
  public allContacts$ = this._store.select(selectAllContacts);
  private _dialogRef: DynamicDialogRef | undefined;

  constructor(
    private readonly _store: Store<AppState>,
    private readonly _dialogService: DialogService,
    private _confirmationService: ConfirmationService,
    private _messageService: MessageService) {
  }

  public ngOnInit(): void {
    this._store.dispatch(loadContacts());
  }

  public addContact(): void {
    this._dialogRef = this._openDialog(undefined, { header: 'Add contact' });

    this._dialogRef.onClose.subscribe((contact: Contact) => {
      if (contact) {
        this._store.dispatch(contactActions.createContact({ contact }));
      }
    });
  }

  public editContact(contact: Contact): void {
    this._dialogRef = this._openDialog(contact, { header: 'Edit contact' });

    this._dialogRef.onClose.subscribe((contact: Contact) => {
      if (contact) {
        this._store.dispatch(contactActions.updateContact({ contact }));
      }
    });
  }

  public deleteContact(contactId: number) {
    this._confirmationService.confirm({
      key: 'delete-contact-confirmation',
      message: 'Do you want to delete this contact?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this._store.dispatch(contactActions.deleteContact({ id: contactId }));
        this._messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'Contact deleted' });
      },
      reject: (type: ConfirmEventType) => {
        switch (type) {
          case ConfirmEventType.REJECT:
            this._messageService.add({ severity: 'error', summary: 'Rejected', detail: 'Deletion rejected' });
            break;
          case ConfirmEventType.CANCEL:
            this._messageService.add({ severity: 'warn', summary: 'Cancelled', detail: 'Deletion cancelled' });
            break;
        }
      }
    });
  }

  public ngOnDestroy(): void {
    if (this._dialogRef) {
      this._dialogRef.close();
    }
  }

  private _openDialog(contact?: Contact, optionals: Partial<DynamicDialogConfig> = {}): DynamicDialogRef {
    const config: DynamicDialogConfig = {
      height: '80%',
      width: '75%',
      contentStyle: { overflow: 'auto' },
      baseZIndex: 9999,
      maximizable: true,
      ...optionals
    };

    return this._dialogService.open(ContactDetailsComponent, {
      ...config,
      data: {
        contact
      }
    });
  }
}
