import { Component, Input, OnInit } from '@angular/core';
import { Contact } from '../models/contact';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css'],
})
export class ContactListComponent implements OnInit {
  @Input() contactList!: Contact[];
  filteredList!: Contact[];
  selectedContact?: Contact;
  isAdd: boolean = false;
  constructor() {}

  ngOnInit(): void {
    this.filteredList = this.contactList;
  }

  onClick(contact: Contact) {
    this.selectedContact = contact;
  }

  deleteContact(deletedContact: Contact) {
    if (this.selectedContact) {
      this.filteredList = this.filteredList.filter(
        (contacto) => contacto !== deletedContact
      );
      this.contactList = this.contactList.filter(
        (contacto) => contacto !== deletedContact
      );
    }
  }

  addContact(contactToAdd: Contact) {
    this.contactList.push(contactToAdd);
    this.filteredList = this.contactList;
  }

  filterContacts(filterInfo: string) {
    if (filterInfo == '') {
      this.filteredList = this.contactList;
      return;
    }
    this.filteredList = this.contactList.filter((contact: Contact) => {
      return (
        contact.name.toLowerCase().includes(filterInfo.toLowerCase()) ||
        contact.email.toLowerCase().includes(filterInfo.toLowerCase()) ||
        contact.phoneNumber.toString().includes(filterInfo)
      );
    });
  }
}
