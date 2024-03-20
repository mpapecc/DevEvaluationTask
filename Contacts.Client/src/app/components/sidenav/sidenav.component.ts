import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { SearchContact } from 'src/app/models/Contact';
import { PostTag, Tag } from 'src/app/models/Tag';
import { ContactService } from 'src/app/services/contact.service';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent implements OnInit {

  isInputVisible:boolean = false;
  isTagValueEmpty:boolean = false;
  newTagValue:string = "";
  @Output() contactSearchEvent = new EventEmitter<SearchContact>();
  

  get tags() {
    return this.contactService.contactSearchForm.get('contactTags') as FormArray;
  }

  constructor(
    private fb: FormBuilder,
    public contactService: ContactService,
    public tagService:TagService) {}

  
  ngOnInit(): void {
    this.tagService.getTags().subscribe(tags=>this.tagService.allTags = tags);
  }

  postNewTag(){
    if(!this.newTagValue.length){
      this.isTagValueEmpty = true
      return;
    }
    let newTag:PostTag={ value :this.newTagValue }
    this.tagService.postTag(newTag).subscribe(tag=>{
      this.tagService.allTags.push(tag)
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
  
  selectTag(tag:Tag){
    if(this.contactService.contactSearchForm.value.contactTags?.includes(tag.id)){
      let indexInArray = this.contactService.contactSearchForm.value.contactTags?.indexOf(tag.id)
      this.contactService.contactSearchForm.controls.contactTags.removeAt(indexInArray)
      return
    }
    (<FormArray>this.tags!).push(this.fb.control(tag.id,Validators.required));
    console.log(this.contactService.contactSearchForm.controls.contactTags.controls)
  }

}
