import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { Contact } from '../models/contact';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {
  private readonly _contactsUrl = `${environment.apiBaseUrl}contacts`;

  constructor(private _http: HttpClient) { }

  public getAll(): Observable<Contact[]> {
    return this._http.get<Contact[]>(this._contactsUrl)
      .pipe(
        catchError(error => { throw error; })
      );
  }

  public create(contact: Contact): Observable<Contact> {
    return this._http.post<Contact>(this._contactsUrl, contact)
      .pipe(
        catchError(error => { throw error; })
      );
  }

  public update(contact: Contact): Observable<Contact> {
    return this._http.put<Contact>(`${this._contactsUrl}/${contact.id}`, contact)
      .pipe(
        catchError(error => { throw error; })
      );
  }

  public delete(id: number): Observable<any> {
    return this._http.delete(`${this._contactsUrl}/${id}`)
      .pipe(
        catchError(error => { throw error; })
      );
  }
}
