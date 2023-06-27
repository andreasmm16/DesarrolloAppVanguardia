import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-contacts-filter',
  templateUrl: './contacts-filter.component.html',
  styleUrls: ['./contacts-filter.component.css'],
})
export class ContactsFilterComponent implements OnInit {
  @Output() filtered = new EventEmitter<string>();
  inputValue:string="";
  constructor() {}

  ngOnInit(): void {}

  onChange() {
    this.filtered.emit(String(this.inputValue));
  }
}
