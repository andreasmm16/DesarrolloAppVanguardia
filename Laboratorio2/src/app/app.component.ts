import { Component } from '@angular/core';
import { Contact } from './models/contact';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ContactsLab2';
  contacts: Contact[] =[];
  selectedContact?: Contact;
  ngOnInit(): void {
    const newContact1: Contact = {
      name: 'Andrea Aguirre',
      email: 'andreasmm@unitec.edu',
      phoneNumber: 97486475,
      title: 'Otro',
    };
    const newContact2: Contact = {
      name: 'Vicente Jimeno',
      email: 'vijimeno@unitec.edu',
      phoneNumber: 95959595,
      title: 'Sr',
    };
    this.contacts.push(newContact1);
    this.contacts.push(newContact2);
  }

 
}
