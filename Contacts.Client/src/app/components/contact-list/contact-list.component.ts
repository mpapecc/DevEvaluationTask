import { Component, Input } from '@angular/core';
import { Contact } from 'src/app/models/Contact';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent{
  @Input() contactList:Contact[] = []
  @Input() listTitle:string = "";

}
