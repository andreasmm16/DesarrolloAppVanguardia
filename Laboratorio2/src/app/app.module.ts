import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ContactListComponent } from './contact-list/contact-list.component';
import { ContactInfoComponent } from './contact-info/contact-info.component';
import { AddContactComponent } from './add-contact/add-contact.component';
import { ContactsFilterComponent } from './contacts-filter/contacts-filter.component';

@NgModule({
  declarations: [
    AppComponent,
    ContactListComponent,
    ContactInfoComponent,
    AddContactComponent,
    ContactsFilterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
