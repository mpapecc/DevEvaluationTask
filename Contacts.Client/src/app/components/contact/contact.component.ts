import { Component, Input } from '@angular/core';
import { Contact } from 'src/app/models/Contact';
import { ContactService } from 'src/app/services/contact.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent{
  @Input() contact:Contact = {} as Contact;

  constructor(public contactService:ContactService) {}


  public getInitials(fullName:string) :string {
    let nameArray = fullName.split(" ");
    let initials:string = "";
    if(nameArray.length<=3){
      nameArray.forEach((e,i) => {
        initials += e.charAt(0).toUpperCase() + " ";
      });
    }else{
      initials += nameArray[0].charAt(0).toUpperCase() + " " + nameArray[nameArray.length-1].charAt(0).toUpperCase();
    }
    return initials;
  }
}
