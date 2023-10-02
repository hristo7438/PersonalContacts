import { createReducer, on } from "@ngrx/store";
import { Contact } from "src/app/models/contact";
import * as contactActions from "./contact.actions";

export interface ContactState {
    contacts: Contact[];
    error: string | null;
    status: 'pending' | 'loading' | 'error' | 'success';
}

export const initialState: ContactState = {
    contacts: [],
    error: null,
    status: 'pending'
}

export const contactReducer = createReducer(
    initialState,
    on(contactActions.createContact, (state, { contact }) => ({
        ...state,
        contacts: [...state.contacts, contact]
    })),
    on(contactActions.updateContact, (state, { contact }) => ({
        ...state,
        contacts: [...state.contacts.filter(c => c.id !== contact.id), contact]
    })),
    on(contactActions.deleteContact, (state, { id }) => ({
        ...state,
        contacts: [...state.contacts.filter(c => c.id !== id)]
    })),
    on(contactActions.loadContacts, (state) => ({
        ...state,
        status: 'loading' as 'loading'
    })),
    on(contactActions.loadContactsSuccess, (state, { contacts }) => ({
        ...state,
        contacts: contacts,
        error: null,
        status: 'success' as 'success'
    })),
    on(contactActions.loadContactsFailure, (state, { error }) => ({
        ...state,
        status: "error" as 'error',
        error: error
    })),

);