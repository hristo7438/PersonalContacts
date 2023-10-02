import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { ContactsService } from "src/app/services/contacts.service";
import { Action } from "@ngrx/store";
import * as contactActions from './contact.actions';
import { Observable, map, catchError, of, switchMap } from "rxjs";
import { Contact } from "src/app/models/contact";

@Injectable()
export class ContactEffects {
    constructor(
        private _actions$: Actions,
        private _contactsService: ContactsService
    ) { }

    public loadContacts$: Observable<Action> = createEffect(() =>
        this._actions$.pipe(
            ofType(contactActions.loadContacts),
            switchMap(() => this._contactsService.getAll().pipe(
                map((contacts: Contact[]) => contactActions.loadContactsSuccess({ contacts: contacts })),
                catchError((error) => of(contactActions.loadContactsFailure({ error: error })))
            ))
        )
    );

    public createContact$ = createEffect(() =>
        this._actions$.pipe(
            ofType(contactActions.createContact),
            switchMap(({ contact }) => this._contactsService.create(contact))
        ),
        { dispatch: false }
    );

    public updateContact$ = createEffect(() =>
        this._actions$.pipe(
            ofType(contactActions.updateContact),
            switchMap(({ contact }) => this._contactsService.update(contact))
        ),
        { dispatch: false }
    );

    public deleteContact$ = createEffect(() =>
        this._actions$.pipe(
            ofType(contactActions.deleteContact),
            switchMap(({ id }) => this._contactsService.delete(id))
        ),
        { dispatch: false }
    );
}