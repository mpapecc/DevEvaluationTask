import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PostTag, Tag } from '../models/Tag';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  allTags:Tag[]=[]
  constructor(public http :HttpClient) { }

  getTags(): Observable<Tag[]>{
    return this.http.get<Tag[]>(`${environment.apiUrl}/Tag`);
  }

  postTag(tag:PostTag) : Observable<Tag>{
    return this.http.post<Tag>(`${environment.apiUrl}/Tag`,tag)
  }
}
