import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Contact } from '../models/contact';

@Component({
  selector: 'app-contact-info',
  templateUrl: './contact-info.component.html',
  styleUrls: ['./contact-info.component.css'],
})
export class ContactInfoComponent implements OnInit {
  @Input() contact?: Contact;
  @Output() deletedContact = new EventEmitter<Contact>(); 
  @Output() contactToAdd = new EventEmitter<Contact>();
  isAdd: boolean = false;

  constructor() {}

  ngOnInit(): void {}

  deleteContact(contact:Contact){
    this.deletedContact.emit(contact);
  }
  
 showAddContact(){
    this.contact=undefined;
    this.isAdd=true;
  }

  transferAddContact(newContact:Contact){
    this.contactToAdd.emit(newContact);
  }

  hideAddComponent(hideAdd:boolean){
    this.isAdd=hideAdd;
  }

}
