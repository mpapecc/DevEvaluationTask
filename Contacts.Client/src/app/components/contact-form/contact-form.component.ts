import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormArray, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, forkJoin } from 'rxjs';
import { Contact, PostContact } from 'src/app/models/Contact';
import { PostTag, Tag } from 'src/app/models/Tag';
import { ContactService } from 'src/app/services/contact.service';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.css']
})
export class ContactFormComponent implements OnInit {
  contact: Contact = {} as Contact;
  showTagList:boolean=false;
  isNewContact: boolean = true;
  allUnusedTags:any[]=[]
  requests:Observable<any>[]= []
  showValidation : boolean = false
  isInputVisible:boolean = false;
  isTagValueEmpty:boolean = false;
  newTagValue:string = "";
  isFormChanged:boolean = false;
  get emails() {
    return this.contactForm.get('emails') as FormArray;
  }

  get numbers() {
    return this.contactForm.get('numbers') as FormArray;
  }

  setEmailControls() {
    let emailsFormArray = new FormArray<FormControl<string | null>>([]);
    this.contact.emails.forEach(email => {
      (emailsFormArray).push(this.fb.control(email.value,Validators.required))
    })
    this.contactForm.setControl("emails", emailsFormArray);
  }

  setNumberControls() {
    let numbersFormArray = new FormArray<FormControl<string | null>>([]);
    this.contact.numbers.forEach(number => {
      (numbersFormArray).push(this.fb.control(number.value,Validators.required))
    })
    this.contactForm.setControl("numbers", numbersFormArray);
  }

  contactForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    address: ['', Validators.required],
    isBookmarked: [false, Validators.required],
    emails: this.fb.array([this.fb.control('',[Validators.required,Validators.email])]),
    numbers: this.fb.array([this.fb.control('',[Validators.required,Validators.pattern("^(?:([0-9]{1})*[- .(]*([0-9]{3})[- .)]*[0-9]{3}[- .]*[0-9]{4})+$")])]),

  });

  constructor(
    private fb: FormBuilder,
    private contactService: ContactService,
    public tagService: TagService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.contact.emails = []
    this.contact.numbers = []
    this.contact.contactTags = []

    this.route.params.subscribe(params => {
      
      if (!params['id']) {
        this.tagService.getTags().subscribe(tags=>this.allUnusedTags = tags)
        return;
      }
      this.isNewContact = false
      this.requests.push(this.tagService.getTags())
      this.requests.push(this.contactService.getContactById(params['id']))

      forkJoin(this.requests).subscribe(request=>{
        this.contact = request[1];
        this.contactForm.patchValue(this.contact)
        this.setEmailControls()
        this.setNumberControls()
        request[0].forEach((tag:any)=>{
          if(this.isTagUnused(tag)){
            this.allUnusedTags.push(tag)
          }
        })
      })
    });
  }

  addEmail() {
    (<FormArray>this.emails!).push(this.fb.control('',[Validators.required,Validators.email]));
    this.contact.emails.push({
      isNew:true,
      value:''
    })
  }

  addNumber() {
    (<FormArray>this.numbers!).push(this.fb.control('',[Validators.required,Validators.pattern("^(?:([0-9]{1})*[- .(]*([0-9]{3})[- .)]*[0-9]{3}[- .]*[0-9]{4})+$")]));
    this.contact.numbers.push({
      isNew:true,
      value:''
    })
  }

  placeholder(i: number, inputName: string) {
    return inputName + " " + i.toString()
  }

  changeTagListVisibility(){
    this.showTagList = !this.showTagList
    this.changeNewTagInputVisibility()
  }

  removeTagFromContact(tagId:string){
    let contactTagIndex = this.contact.contactTags.findIndex(ct=>ct.tag.id === tagId)
    let tag = this.contact.contactTags[contactTagIndex].tag

    this.allUnusedTags.push(tag)
    this.contact.contactTags.splice(contactTagIndex,1)
    console.log(this.contact.contactTags)
    console.log(this.allUnusedTags)

  }

  addTagToContact(tagId:string){
    let contactTagIndex = this.allUnusedTags.findIndex(ct=>ct.id === tagId)
    let newContactTag={
      contactId:this.contact.id,
      tagId:this.allUnusedTags[contactTagIndex].id,
      tag:this.allUnusedTags[contactTagIndex]
    }
    this.contact.contactTags.push(newContactTag)
    this.allUnusedTags.splice(contactTagIndex,1)
  }
  removeControl(controlType: string, index: number) {
    if(controlType === "numbers"){
        this.numbers.removeAt(index)
        this.contact.numbers.splice(index,1)    
    }

    if(controlType === "emails"){
      this.emails.removeAt(index)
      this.contact.emails.splice(index,1)      
      }
  }

  createNewContact() {
    let contact: PostContact = {
      firstName: this.contactForm.value.firstName!,
      lastName: this.contactForm.value.lastName!,
      address: this.contactForm.value.address!,
      isBookmarked: this.contactForm.value.isBookmarked!,
      contactTags:this.contact.contactTags.map(x => ({ tagId: x.tagId })),
      emails: this.contactForm.value.emails!.map(x => ({ value: x })),
      numbers: this.contactForm.value.numbers!.map(x => ({ value: x }))
    }
       this.contactService.postContact(contact).subscribe({
      next: () => this.router.navigate([""])
    })

  }

  editExistingConact() {
    let contactForEdit :Contact ={
      id:this.contact.id,
      firstName:this.contactForm.value.firstName!,
      lastName:this.contactForm.value.lastName!,
      address:this.contactForm.value.address!,
      isBookmarked:this.contactForm.value.isBookmarked!,
      contactTags:this.contact.contactTags.map(x => ({ tagId: x.tagId })),
      emails : this.mapEmailsForContactEdit(),
      numbers: this.mapNumbersForContactEdit()
    }
    this.contactService.putContact(contactForEdit).subscribe({
      next:() => this.router.navigate([""])
    })
  }

  mapEmailsForContactEdit():any[]{
    let emails:any[]=[]
    this.contact.emails.map((email,i)=>{
      email.value = this.contactForm.value.emails![i]
      if(email.isNew){
        email.contactId=this.contact.id
        delete email.isNew
      }
      emails.push(email)
    })
    return emails
  }

  mapNumbersForContactEdit():any[]{
    let numbers : any[] = []
    this.contact.numbers.map((number,i)=>{
      number.value = this.contactForm.value.numbers![i]
      if(number.isNew){
        number.contactId=this.contact.id
        delete number.isNew
      }
      numbers.push(number)
    })
    return numbers;
  }

  onSubmit() {
    if(!this.contactForm.valid){
      this.showValidation = true;
      return;
    }
    this.isFormChanged = true;
    if (this.isNewContact) {
      this.createNewContact()
      return;
    }
    this.editExistingConact()
  }

  isTagUnused(tag:Tag){
    return this.contact.contactTags.findIndex(t=>t.tagId ===tag.id) === -1
  }

  postNewTag(){
    if(!this.newTagValue.length){
      this.isTagValueEmpty = true
      return;
    }
    let newTag:PostTag={ value :this.newTagValue }
    this.tagService.postTag(newTag).subscribe(tag=>{
      this.tagService.allTags.push(tag)
      let newContactTag={
        contactId:this.contact.id,
        tagId:tag.id,
        tag:tag
      }
      this.contact.contactTags.push(newContactTag)
      this.newTagValue = "";
    });
  }

  changeNewTagInputVisibility(){
    this.isInputVisible = !this.isInputVisible
    this.newTagValue = "";
    this.isTagValueEmpty = false

  }

  newTagInputChange(e:any){
    if(e){
      this.isTagValueEmpty = false
      return;
    }
    this.isTagValueEmpty = true
  }
}
