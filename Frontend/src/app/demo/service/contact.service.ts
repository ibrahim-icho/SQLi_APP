import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Contact } from '../models/Contact';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private cachedContact: Contact | null = null;
  apiBaseUrl: string;
  contactId: string;

  constructor(private http: HttpClient) { 
    this.apiBaseUrl = environment.apiUrl;
    this.contactId = environment.contactId;
  }

  getContactLogedInData(): Observable<Contact> {
    if (this.cachedContact) {
      return of(this.cachedContact);
    }

    const apiUrl = `${this.apiBaseUrl}Contact/GetContactById/${this.contactId}`;
    return this.http.get<Contact>(apiUrl);
  }

  cacheContact(contact: Contact): void {
    this.cachedContact = contact;
  }

  getCachedContact(): Contact | null {
    return this.cachedContact;
  }
}
