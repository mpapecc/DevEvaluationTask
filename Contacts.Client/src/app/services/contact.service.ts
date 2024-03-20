import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contact, PostContact, SearchContact } from '../models/Contact';
import { FormBuilder } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  bookmarkedContacts : Contact[]=[]
  allContacts : Contact[]=[]
  isLoading:boolean = false
  contactSearchForm = this.fb.group({
    firstName: [''],
    lastName: [''],
    contactTags: this.fb.array([]),

  });
  
  contactSearchData:SearchContact = {
    firstName:"",
    lastName:"",
    contactTags :[]
  }

  constructor(public http:HttpClient,private fb: FormBuilder) { }

  searchContacts(searchContact:SearchContact | null = null): Observable<Contact[]> 
    {
    return this.http.get<Contact[]>(`${environment.apiUrl}/Contact/search?${this.contactSearchQueryString(searchContact!)}`);
    }

  getContactById(contactId:string): Observable<Contact> {
    return this.http.get<Contact>(`${environment.apiUrl}/Contact/${contactId}`);
  }

  putContact(contact: Contact): Observable<Contact> {
    return this.http.put<Contact>(`${environment.apiUrl}/Contact/${contact.id}`, contact)
  }

  postContact(contact: PostContact): Observable<Contact> {
    return this.http.post<Contact>(`${environment.apiUrl}/Contact`, contact)
  }

  deleteContact(contactId:string): Observable<null> {
    return this.http.delete<null>(`${environment.apiUrl}/Contact/${contactId}`)
  }

  sortContacListsAlphabeticaly(contactList:Contact[]){
    contactList.sort((a, b) => a.firstName.localeCompare(b.firstName))
  }

  changeBookmarkStatus(contact:Contact){
    let contactChanging = this.allContacts.find(c=>c.id === contact.id)
    if(contactChanging === undefined){
      return
    }
    contactChanging!.isBookmarked = !contactChanging!.isBookmarked  
    this.putContact(contactChanging).subscribe()

    if(contactChanging!.isBookmarked){
      this.bookmarkedContacts.push(contact)
    }

    if(!contactChanging!.isBookmarked){
      let indexOfContactInList = this.bookmarkedContacts.indexOf(contact)
      this.bookmarkedContacts.splice(indexOfContactInList,1)
    } 
    this.sortContacListsAlphabeticaly(this.bookmarkedContacts)

  }

  deleteContactImp(id:string){
    this.deleteContact(id).subscribe()
    let indexOfContactInAllList = this.allContacts.findIndex(c=>c.id === id)
    if(this.allContacts[indexOfContactInAllList].isBookmarked){
      let indexOfContactInBookmarkedList = this.bookmarkedContacts.findIndex(c=>c.id === id)
      this.bookmarkedContacts.splice(indexOfContactInBookmarkedList,1);
    }
    this.allContacts.splice(indexOfContactInAllList,1);
  }


  contactSearchQueryString(searchContact:SearchContact){
    if(searchContact === null){
      return
    }
    return `${this.firstNameQuieryString(searchContact.firstName)}${this.lastNameQuieryString(searchContact!.lastName)}${this.tagsQueryString(searchContact!.contactTags)}`
  }

  firstNameQuieryString(firstName:string | null){
    if(firstName === null){
      return
    }
    return `&firstName=${firstName}`
  }

  lastNameQuieryString(lastName:string | null){
    if(lastName === null){
      return
    }
    return `&lastName=${lastName}`
  }

  tagsQueryString(tags:string[] | null){
    if(tags===null){
      return;
    }
    let query = ""
    for (let i = 0; i < tags.length; i++) {
      query += `&tags=${tags[i]}`
    }
    return query  
  }
}
