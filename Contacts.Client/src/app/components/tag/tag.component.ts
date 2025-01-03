import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html',
  styleUrls: ['./tag.component.css']
})
export class TagComponent {
  @Input() text:string=""
  @Input() isActive:boolean = false

  activateTag(){
    this.isActive = !this.isActive
  }
}
