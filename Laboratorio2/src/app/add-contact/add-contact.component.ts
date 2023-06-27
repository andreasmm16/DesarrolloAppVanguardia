import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Contact } from '../models/contact';


@Component({
  selector: 'app-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrls: ['./add-contact.component.css']
})
export class AddContactComponent implements OnInit {
  titles: string[] = ['Sr.','Sra.','Otro']
  form!: FormGroup;
  @Input() isAdd :boolean = true;
  @Output() addedContact = new EventEmitter<Contact>();
  @Output() hideAdd = new EventEmitter<boolean>();
  constructor() {
    
  }
  ngOnInit(): void {
    this.form = this.initForm();
  }
  initForm(): FormGroup<any> {
    return new FormGroup({
      name: new FormControl("", [Validators.required, Validators.minLength(3)]),
      email: new FormControl("", [Validators.email, Validators.required]),
      phone: new FormControl("", Validators.required),
      title: new FormControl("Sr.")
    });
  }

  onSubmit():void{
    const name = this.form.get('name')?.value;
    const email = this.form.get('email')?.value;
    const phoneNumber = this.form.get('phone')?.value;
    const title = this.form.get('title')?.value;

    const newContact: Contact = {
      name: name,
      email: email,
      phoneNumber: phoneNumber,
      title: title
    };
    this.addedContact.emit(newContact);
    this.form.reset();
    this.hideAdd.emit(false);
  }

  get formControls(){
    return this.form.controls;
  }

}
