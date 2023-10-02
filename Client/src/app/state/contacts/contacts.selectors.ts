import { createSelector } from "@ngrx/store";
import { AppState } from "../app.state";
import { ContactState } from "./contact.reducer";

export const selectAllContacts = createSelector(
    (state: AppState) => state.contacts,
    (state: ContactState) => state.contacts
)