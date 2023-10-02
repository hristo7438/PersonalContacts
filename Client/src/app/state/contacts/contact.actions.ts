import { createAction, props } from "@ngrx/store";
import { Contact } from "src/app/models/contact";

export const createContact = createAction(
    '[Contact] Create Contact',
    props<{ contact: Contact }>()
);

export const updateContact = createAction(
    '[Contact] Update Contact',
    props<{ contact: Contact }>()
);

export const deleteContact = createAction(
    '[Contact] Delete Contact',
    props<{ id: number }>()
);

export const loadContacts = createAction(
    '[Contact] Load Contacts'
);

export const loadContactsSuccess = createAction(
    '[Contact] Load Contacts Success',
    props<{ contacts: Contact[] }>()
);

export const loadContactsFailure = createAction(
    '[Contact] Load Contacts Failure',
    props<{ error: string }>()
);