import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';
import { ContactService } from './contact.service';
import { Contact } from '../models/Contact';

@Injectable({
  providedIn: 'root'
})
export class ContactResolverService implements Resolve<Contact> {

  constructor(private contactService: ContactService) {}

  resolve(): Observable<Contact> {
    return this.contactService.getContactLogedInData();
  }


}
