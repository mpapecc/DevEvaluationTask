import { Component, OnInit } from '@angular/core';
import { ContactService } from 'src/app/services/contact.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit{

  timeout:any

  constructor(public contactService:ContactService) {}

  ngOnInit(): void {
    this.searchContacts(this.contactService.contactSearchData)
    this.listenForContactSearchFormChange()
  }
  
  searchContacts(contactSearchData:any){
    this.contactService.searchContacts(contactSearchData).subscribe(contacts=>{
      this.contactService.allContacts = contacts
      this.contactService.bookmarkedContacts = []
      this.contactService.allContacts.forEach(contact=>{
        if(contact.isBookmarked){
          this.contactService.bookmarkedContacts.push(contact)
        }
      })
    })
  }

  listenForContactSearchFormChange(){
    this.contactService.contactSearchForm.valueChanges.subscribe(contactSearch=>{
      clearTimeout(this.timeout)
      this.contactService.contactSearchData.firstName = contactSearch.firstName!
      this.contactService.contactSearchData.lastName = contactSearch.lastName!
      this.contactService.contactSearchData.contactTags = contactSearch.contactTags!
      this.contactService.isLoading = true
      this.timeout = setTimeout(() => {
        this.searchContacts(this.contactService.contactSearchData)
        this.contactService.isLoading = false

      }, 1500);
    })
  }
}
